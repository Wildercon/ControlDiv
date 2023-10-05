using ControlDiv.API.Data;
using ControlDiv.Shared.Entities;
using ControlDiv.Shared.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Microsoft.VisualBasic;

namespace ControlDiv.API.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly DataContext _dataContext;

        public VoucherRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string> AddVoucherAndUpdateAccount(Voucher voucher)
        {

            try
            {
                voucher.Date = DateTime.UtcNow;
                if (voucher.NoteType == NoteType.Credito)
                    voucher.Account!.Mont = voucher.Account.Mont + voucher.Mont;
                else if (voucher.NoteType == NoteType.Debito)
                    voucher.Account!.Mont = voucher.Account.Mont - voucher.Mont;
                voucher.Total = voucher.Account!.Mont;
                voucher.Details = voucher.OperationType.GetDisplayName() + voucher.Details;
                _dataContext.Add(voucher);
                _dataContext.Update(voucher.Account);
                var result = await _dataContext.SaveChangesAsync();
                if (result > 0)
                    return "";
                else
                    return "Error Inesperado";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public async Task<List<Voucher>> GetVouchers()
        {
            return  await _dataContext.Vouchers.Include(x => x.Account).OrderByDescending(x=> x.Id).ToListAsync();
        }
    }
}
