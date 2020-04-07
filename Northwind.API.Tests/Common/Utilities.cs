using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Northwind.API.Tests.Common
{
    public class Utilities
    {
        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
    }
}
