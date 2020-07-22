using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using ShoppingDirectory.Common;
using ShoppingDirectory.Services;

namespace ShoppingDirectory.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<ShoppingDirectory.App>("app");

            builder.Services.AddScoped<IToastService, ToastService>();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<IStoreDataService, StoreDataService>
                (client => client.BaseAddress = new Uri("https://localhost:44363/"));

            builder.Services.AddHttpClient<IStoreCategoryDataService, StoreCategoryDataService>
                (client => client.BaseAddress = new Uri("https://localhost:44363/"));

            await builder.Build().RunAsync();
        }
    }
}
