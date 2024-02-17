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
                            if (saleDTO.MontSale <= calc)
                            {
                                var sale = new Sale()
                                {
                                    Mont = saleDTO.MontSale,
                                    Date = DateTime.UtcNow,
                                    Total = saleDTO.MontSale + user.Mont,
                                    User = user,
                                    Details = $"{saleDTO.Details} codigo{voucher.Code}"

                                };
                                if(saleDTO.Customer != null)
                                {
                                    var customer = saleDTO.Customer;
                                    var customerDetail = new CustomerDetail()
                                    {
                                        Customer = customer,
                                        Mont = saleDTO.MontSale,
                                        Date = DateTime.UtcNow,
                                        Total = customer.Mont + saleDTO.MontSale,
                                        NoteType = NoteType.Credito
                                    };
                                    customer.Mont = customerDetail.Total;
                                    await _context.AddAsync(customerDetail);
                                    _context.Update(customer);
                                    user.MontDeliver = user.MontDeliver + saleDTO.MontSale;
                                }
                                
                                user.Mont = user.Mont + sale.Mont;
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
                        User = user,
                        Details = $"Devolucion {saleDTO.Details} codigo {saleDTO.VoucherCode}"
                    };
                    user.Mont = sale.Total;                   
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
        public async Task<string> DeliverOrReceiver(DeliverOrReceiveDTO receiveDTO)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == receiveDTO.IdUser);
                    var account = _context.Accounts.FirstOrDefault(x => x.Id == 3);
                    var sale = new Sale()
                    {
                        Mont = receiveDTO.Mont,
                        Date = DateTime.UtcNow,                      
                        User = user,
                        Details = $"Efectivo {receiveDTO.Details}"
                    };
                    var vouch = new Voucher()
                    {
                        Code = "Efectivo",
                        Mont = receiveDTO.Mont,
                        Account = account,
                        Details = $"{receiveDTO.Mont}",                        
                        OperationType = OperationType.Efectivo
                    };
                    if (receiveDTO.IsDeliver)
                    {
                        sale.Total = user!.Mont - receiveDTO.Mont;
                        vouch.NoteType = NoteType.Debito;
                    }else
                    {
                        sale.Total = user!.Mont + receiveDTO.Mont;
                        vouch.NoteType = NoteType.Credito;
                    }
                    user.Mont = sale.Total;
                    _context.Update(user);
                    _context.Add(sale);
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
        public async Task<string> AddZelle(ZelleDTO zelleDTO,User user)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    var voucher = _context.Vouchers.FirstOrDefault(x => x.Code == zelleDTO.Codigo);
                    if (voucher != null)
                    {
                        if (voucher.OperationType == OperationType.sinOperar)
                        {
                            var precioD = _context.Prices.FirstOrDefault();
                            
                            if (zelleDTO.Mont == voucher.Mont)
                            {
                                var descuento = (zelleDTO.Mont * zelleDTO.Percentage) / 100;
                                var sale = new Sale()
                                {
                                    Mont = zelleDTO.Mont - descuento,
                                    Date = DateTime.UtcNow,
                                    Total = (zelleDTO.Mont-descuento) + user.Mont,
                                    User = user,
                                    Details = $"Zelle {zelleDTO.Mont} {zelleDTO.Details} codigo{voucher.Code}"
                                };
                                user.Mont = user.Mont + sale.Mont;                               
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
                                
                                return "Hay disparidad en monto de venta.";
                            }

                        }
                        else
                        {
                            return "Ya existe una operación con este codigo";
                        }

                    }
                    else
                    {
                        //await _temporalSaleRepository.Add(zelleDTO, user);
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
