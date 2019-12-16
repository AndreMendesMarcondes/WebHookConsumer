using System;
using System.Net.Http;
using System.Text;

namespace QuoteConsumer
{
    public static class ApiService
    {
        public static async void Post(Quote quote)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (HttpClient client = new HttpClient(clientHandler))
            {
                try
                {
                    Uri uri = new Uri(Environment.GetEnvironmentVariable("PostURL"));
                    await client.PostAsync(uri, new StringContent(quote.ToString(), Encoding.UTF8, "application/json"));
                }
                catch (System.Net.Http.HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
