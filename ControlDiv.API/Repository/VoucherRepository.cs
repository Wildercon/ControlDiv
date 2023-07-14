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
            using (var trans = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    voucher.Date = DateTime.Now;
                    if (voucher.TypeVoucher == "Credito")
                        voucher.Account!.Mont = voucher.Account.Mont + voucher.Mont;
                    else if (voucher.TypeVoucher == "Debito")
                        voucher.Account!.Mont = voucher.Account.Mont - voucher.Mont;
                    voucher.OperationType = OperationType.sinOperar;
                    voucher.Total = voucher.Account!.Mont;
                    voucher.Observation = voucher.OperationType.GetDisplayName()+voucher.Observation;

                    _dataContext.Add(voucher);

                    _dataContext.Update(voucher.Account);
                    await _dataContext.SaveChangesAsync();
                    trans.Commit();
                    return "";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return ex.ToString();
                }

            }
        }

        public async Task<List<Voucher>> GetVouchers()
        {
            return  await _dataContext.Vouchers.Include(x => x.Account).ToListAsync();
        }
    }
}
