using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebHookConsumer
{
    class ApiService
    {
        public async void Post(Quote quote)
        {
            using (var client = new HttpClient())
            {
                Uri uri = new Uri("https://localhost:44315/quote");
                await client.PostAsync(uri, new StringContent(quote.ToString(), Encoding.UTF8, "application/json"));
            }
        }
    }
}
