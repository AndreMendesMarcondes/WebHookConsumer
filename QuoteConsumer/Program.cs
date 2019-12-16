using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuoteConsumer
{
    class Program
    {
        private static readonly UTF8Encoding encoding = new UTF8Encoding();

        public static List<Quote> QuotesList { get; set; }

        static void Main(string[] args)
        {
            try
            {
                QuotesList = new List<Quote>();
                Connect(Environment.GetEnvironmentVariable("HookURL")).Wait();
            }
            catch (Exception ex)
            {
                
            }
        }

        public static async Task Connect(string uri)
        {
            Thread.Sleep(1000);

            ClientWebSocket webSocket = null;
            try
            {
                webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                await Task.WhenAll(Receive(webSocket));
            }
            catch (Exception ex)
            {
                Console.WriteLine("WebSocket error. " + ex.Message);
            }
            finally
            {
                if (webSocket != null)
                {
                    webSocket.Dispose();
                }

                Console.WriteLine();
                Console.WriteLine("WebSocket closed.");
            }
        }

        private static async Task Receive(ClientWebSocket webSocket)
        {
            byte[] buffer = new byte[1024];
            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    string message = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                    Quote quote = new Quote(message);
                    ApiService.Post(quote);
                }
            }
        }
    }
}


