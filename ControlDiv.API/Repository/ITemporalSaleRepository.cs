using ControlDiv.API.Data;
using ControlDiv.Shared.DTOs;
using ControlDiv.Shared.Entities;

namespace ControlDiv.API.Repository
{
    public interface ITemporalSaleRepository
    {
        Task<bool> Add(SaleDTO saleDTO, User user);        
        Task<string> AuthorizeTemporalSale(TemporalSale saleDTO);        
    }
}
