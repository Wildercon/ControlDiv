using ControlDiv.API.Data;
using ControlDiv.Shared.DTOs;
using ControlDiv.Shared.Entities;
using ControlDiv.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace ControlDiv.API.Repository
{
    public class TemporalSaleRepository : ITemporalSaleRepository
    {
        private readonly DataContext _context;
        private readonly IVoucherRepository _voucherRepository;

        

        public TemporalSaleRepository(DataContext context, IVoucherRepository voucherRepository)
        {
            _context = context;
            _voucherRepository = voucherRepository;
        }

        public async Task<bool> Add(SaleDTO saleDTO,User user)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Id == saleDTO.AccountId);
            var TemSale = new TemporalSale()
            {
                VoucherCode = saleDTO.VoucherCode,
                Montreceived = saleDTO.MontVoucher,
                MontSale = saleDTO.MontSale,
                Account = account,
                Details = saleDTO.Details,
                User = user
            };
            await _context.Temporals.AddAsync(TemSale);
            var result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<string> AuthorizeTemporalSale(TemporalSale temporalSale)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = temporalSale.User;
                    var voucher = _context.Vouchers.FirstOrDefault(x => x.Code == temporalSale.VoucherCode);
                    if (voucher == null)
                    {
                        var vouch = new Voucher()
                        {
                            Code = temporalSale.VoucherCode,
                            Mont = temporalSale.Montreceived,
                            Account = temporalSale.Account,
                            Details = $"{user!.Name} {temporalSale.MontSale}",  
                            NoteType = NoteType.Credito,
                            OperationType = OperationType.Venta
                        };
                        var result = await _voucherRepository.AddVoucherAndUpdateAccount(vouch);
                        if (result != string.Empty)
                            return result;
                    }
                    else
                    {
                        if (voucher.OperationType == OperationType.sinOperar)
                        {
                            voucher!.OperationType = OperationType.Venta;
                            voucher.Details = $"{voucher.OperationType} {user!.Name}  {temporalSale.MontSale}";
                            _context.Vouchers.Update(voucher);
                        }
                        else
                            return "Esta Pago ya tiene una operación";
                    }                  
                    user.Mont = user.Mont + temporalSale.MontSale;

                    var sale = new Sale()
                    {
                        User = user,
                        Mont = temporalSale.MontSale,
                        Details = $"{temporalSale.Details} codigo {temporalSale.VoucherCode}",
                        Date = DateTime.UtcNow,                       
                        Total = user.Mont 
                    };
                    _context.Users.Update(user);
                    await _context.Sales.AddAsync(sale); 
                    await _context.Temporals.Where(x=> x.Id == temporalSale.Id).ExecuteDeleteAsync();
                    await _context.SaveChangesAsync();
                    await trans.CommitAsync();
                    return "";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return ex.Message;                    
                }
            }
        }
            

       
    }
}
