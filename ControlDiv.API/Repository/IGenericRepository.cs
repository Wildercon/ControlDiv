using ControlDiv.Shared.Entities;
using System.Linq.Expressions;

namespace ControlDiv.API.Repository
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> Create(TModel model);
        Task<bool> Update(TModel model);
        Task<bool> Delete(TModel model);
        Task<List<TModel>> GetAll();


    }
}
