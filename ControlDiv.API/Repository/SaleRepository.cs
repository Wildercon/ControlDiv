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
        

        public SaleRepository(DataContext context, IVoucherRepository voucherRepository)
        {
            _context = context;
            
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

                        var sale = new Sale()
                        {
                            Mont = saleDTO.MontSale,
                            Date = DateTime.UtcNow,
                            Total = saleDTO.MontSale + user.Mont,
                            Comission = saleDTO.MontSale + user.Comission,
                            User = user,
                            Details = $"{saleDTO.details}codigo{voucher.Code}"
                        };
                        user.Mont = user.Mont + sale.Mont;
                        user.Comission = user.Comission + sale.Mont;
                        voucher.OperationType = OperationType.Venta;
                        voucher.Details = voucher.OperationType+sale.Details;
                        await _context.Sales.AddAsync(sale);
                        _context.Update(user);
                        _context.Update(voucher);
                        await _context.SaveChangesAsync();
                        trans.Commit();
                        return "";
                    }
                    else
                    {
                        return "Aun no Verifican este pago intente mas tarde";
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
