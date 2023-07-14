using ControlDiv.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControlDiv.API.Repository
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _context.Set<TModel>().Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Update(TModel model)
        {
            try
            {
                _context.Set<TModel>().Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(TModel model)
        {
            try
            {
                _context.Set<TModel>().Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TModel>> GetAll()
        {
           return _context.Set<TModel>().ToList();
        }
    }
}
