using ControlDiv.API.Data;
using ControlDiv.Shared.DTOs;
using ControlDiv.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlDiv.API.Repository
{
    public class TemporalSaleRepository : ITemporalSaleRepository
    {
        private readonly DataContext _context;

        public TemporalSaleRepository(DataContext context)
        {
            _context = context;
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
                Details = saleDTO.details,
                User = user
            };
            await _context.Temporals.AddAsync(TemSale);
            var result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<List<TemporalSale>> GetAll()
        {
            var result = await _context.Temporals.Include(x => x.Account).Include(x => x.User).ToListAsync();
            return result;
        }

        public Task<List<TemporalSale>> GetTemporalUser()
        {
            throw new NotImplementedException();
        }
    }
}
