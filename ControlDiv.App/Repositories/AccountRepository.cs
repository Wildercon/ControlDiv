using ControlDiv.App.Data;
using ControlDiv.Shared.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.App.Repositories
{
    internal class AccountRepository : IAccounRepository
    {

        public async Task<T> Get<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            

        }

        
    }
}
