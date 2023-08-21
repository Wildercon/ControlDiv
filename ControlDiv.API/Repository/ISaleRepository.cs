using ControlDiv.Shared.DTOs;
using ControlDiv.Shared.Entities;

namespace ControlDiv.API.Repository
{
    public interface ISaleRepository
    {
        Task<string> Add(SaleDTO saleDTO,User user);
        Task<string> ReverseSale(SaleDTO saleDTO);
        Task<string> AddZelle(SaleDTO saleDTO, User user);
    }
}
