using ControlDiv.Shared.Entities;

namespace ControlDiv.API.Repository
{
    public interface IVoucherRepository
    {
        Task<string> AddVoucherAndUpdateAccount(Voucher voucher);
        Task<List<Voucher>> GetVouchers();
    }
}
