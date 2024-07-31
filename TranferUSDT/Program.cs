using EthereumLibrary;
using EthereumLibrary.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace TranferUSDT
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {

            // Cấu hình DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IWalletService, WalletService>()
                .AddSingleton<IEthereumService>(provider => new EthereumService(
                    "https://holesky.infura.io/v3/ee33907162f3414ca4021cb7f973652c",
                    "0xe7a2277E367ab9945355e2d7Cff79900644D36f3"
                ))
                .BuildServiceProvider();

            // Lấy instance của IWalletService và IEthereumService từ DI container
            var walletService = serviceProvider.GetService<IWalletService>();
            var ethereumService = serviceProvider.GetService<IEthereumService>();


            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(ethereumService,walletService));
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // Đăng ký các dịch vụ
            services.AddTransient<IEthereumService, EthereumService>();
            services.AddTransient<IWalletService, WalletService>();


            // Đăng ký các form
            services.AddTransient<Form1>();
        }

    }
}