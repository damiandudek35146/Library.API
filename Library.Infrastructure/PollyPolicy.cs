using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class PollyPolicy
    {
        public static AsyncRetryPolicy GetRetryPolicy()
        {
            return Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine($"Retrying due to: {exception.Message}");
                    });
        }
    }
}
