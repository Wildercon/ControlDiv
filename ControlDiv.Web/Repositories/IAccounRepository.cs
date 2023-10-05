using ControlDiv.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.Web.Repositories
{
    public interface IAccounRepository
    {
        Task<T> Get<T>(string url);

        
    }
}
