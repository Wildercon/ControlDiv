using ControlDiv.API.Data;
using ControlDiv.Shared.DTOs;
using ControlDiv.Shared.Entities;
using ControlDiv.Shared.Enum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ControlDiv.API.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DataContext _context;
        private readonly IVoucherRepository _voucherRepository;
        private readonly ITemporalSaleRepository _temporalSaleRepository;

        public SaleRepository(DataContext context, IVoucherRepository voucherRepository, ITemporalSaleRepository temporalSaleRepository)
        {
            _context = context;
            _voucherRepository = voucherRepository;
            _temporalSaleRepository = temporalSaleRepository;
        }

        public async Task<string> Add(SaleDTO saleDTO,User user)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    var voucher = _context.Vouchers.FirstOrDefault(x => x.Code == saleDTO.VoucherCode);
                    if (voucher != null)
                    {
                        if(voucher.OperationType == OperationType.sinOperar)
                        {
                            var precioD = _context.Prices.FirstOrDefault();
                            var calc = voucher.Mont / precioD!.Venta;
                            if (saleDTO.MontSale == calc)
                            {
                                var sale = new Sale()
                                {
                                    Mont = saleDTO.MontSale,
                                    Date = DateTime.UtcNow,
                                    Total = saleDTO.MontSale + user.Mont,
                                    Comission = saleDTO.MontSale + user.Comission,
                                    User = user,
                                    Details = $"{saleDTO.details} codigo{voucher.Code}"
                                };
                                user.Mont = user.Mont + sale.Mont;
                                user.Comission = user.Comission + sale.Mont;
                                voucher.OperationType = OperationType.Venta;
                                voucher.Details = voucher.OperationType + user.Name;
                                await _context.Sales.AddAsync(sale);
                                _context.Update(user);
                                _context.Update(voucher);
                                await _context.SaveChangesAsync();
                                trans.Commit();
                                return "";
                            }
                            else
                            {
                                await _temporalSaleRepository.Add(saleDTO, user);
                                trans.Commit();
                                return "Hay disparidad en monto de venta, El administrador debe Autorizar la Operación";
                            }

                        }
                        else
                        {
                            return "Ya existe una operación con este codigo";
                        }

                    }
                    else
                    {
                        await _temporalSaleRepository.Add(saleDTO, user);
                        trans.Commit();
                        return "Aun no Verifican este pago,Espere que el administrador autorize la operación";
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return ex.ToString();
                }
            }
            
        }

        public async Task<string> ReverseSale(SaleDTO saleDTO)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == saleDTO.IdUser);
                    var sale = new Sale()
                    {
                        Mont = saleDTO.MontSale,
                        Date = DateTime.UtcNow,
                        Total = user!.Mont - saleDTO.MontSale,
                        Comission = user.Comission - saleDTO.MontSale,
                        User = user,
                        Details = $"Devolucion {saleDTO.details} codigo {saleDTO.VoucherCode}"
                    };
                    user.Mont = sale.Total;
                    user.Comission = sale.Comission;                   
                    _context.Update(user);
                    _context.Add(sale);
                    var account = _context.Accounts.FirstOrDefault(x => x.Id == saleDTO.AccountId);
                    var vouch = new Voucher()
                    {
                        Code = saleDTO.VoucherCode,
                        Mont = saleDTO.MontVoucher,
                        Account = account,
                        Details = $"{saleDTO.MontSale} Cod.{saleDTO.VoucherCode}",
                        NoteType = NoteType.Debito,
                        OperationType = OperationType.Devolucion
                    };
                    var result = await _voucherRepository.AddVoucherAndUpdateAccount(vouch);
                    if (result != string.Empty)
                        return result;
                    _context.SaveChanges();
                    trans.Commit();
                    return "";

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return ex.Message;                   
                }               
            }
            
        }
        public async Task<string> AddZelle(SaleDTO saleDTO,User user)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    var voucher = _context.Vouchers.FirstOrDefault(x => x.Code == saleDTO.VoucherCode);
                    if (voucher != null)
                    {
                        if (voucher.OperationType == OperationType.sinOperar)
                        {
                            var precioD = _context.Prices.FirstOrDefault();
                            
                            if (saleDTO.MontSale == voucher.Mont)
                            {
                                var sale = new Sale()
                                {
                                    Mont = saleDTO.MontSale,
                                    Date = DateTime.UtcNow,
                                    Total = saleDTO.MontSale + user.Mont,
                                    Comission = user.Comission,
                                    User = user,
                                    Details = $"Zelle {saleDTO.details} codigo{voucher.Code}"
                                };
                                user.Mont = user.Mont + sale.Mont;
                                user.Comission = user.Comission;
                                voucher.OperationType = OperationType.Zelle;
                                voucher.Details = voucher.OperationType + user.Name;
                                await _context.Sales.AddAsync(sale);
                                _context.Update(user);
                                _context.Update(voucher);
                                await _context.SaveChangesAsync();
                                trans.Commit();
                                return "";
                            }
                            else
                            {
                                await _temporalSaleRepository.Add(saleDTO, user);
                                trans.Commit();
                                return "Hay disparidad en monto de venta, El administrador debe Autorizar la Operación";
                            }

                        }
                        else
                        {
                            return "Ya existe una operación con este codigo";
                        }

                    }
                    else
                    {
                        await _temporalSaleRepository.Add(saleDTO, user);
                        trans.Commit();
                        return "Aun no Verifican este pago,Espere que el administrador autorize la operación";
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return ex.ToString();
                }
            }
        }
    }
}
