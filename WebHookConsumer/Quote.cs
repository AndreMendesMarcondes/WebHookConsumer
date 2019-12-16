using System;

namespace WebHookConsumer
{
    public class Quote
    {
        public string Name { get; private set; }
        public string Value { get; private set; }
        public DateTime Date { get; private set; }

        public Quote(string quote)
        {
            Name = quote.Substring(2, 5);
            Value = quote.Substring(10).Split(',')[0];
            DateTime date = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            Date = date.AddSeconds(Convert.ToDouble(quote.Split(':')[2].Split('}')[0]));
        }
    }
}
