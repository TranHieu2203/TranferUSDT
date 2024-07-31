using System;
using System.Threading.Tasks;
using TransferUsdtLib;
using RabbitMqLib;
using EthereumLibrary;

namespace USDTTransferConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string rabbitMqHost = "localhost";
            string queueName = "usdt_transfer_queue";
            string privateKey = "YOUR_PRIVATE_KEY";
            string infuraUrl = "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID";
            string contractAddress = "0xdac17f958d2ee523a2206206994597c13d831ec7";

            var rabbitMqService = new RabbitMqService(rabbitMqHost);
            var walletService = new WalletService();
            var ethereumService = new EthereumService(infuraUrl, contractAddress);

            // Tạo ví mới
            var wallet = walletService.CreateWallet("");
            var account = wallet.GetAccount(0);

            Console.WriteLine($"Mnemonic: {wallet.Words}");
            Console.WriteLine($"Address: {account.Address}");
            Console.WriteLine($"Private Key: {account.PrivateKey}");

            // Gửi yêu cầu chuyển USDT vào hàng đợi
            string message = $"{account.Address},10.0"; // Giả sử gửi 10 USDT
            rabbitMqService.SendMessage(queueName, message);

            // Nhận và xử lý yêu cầu từ hàng đợi
            rabbitMqService.ReceiveMessage(queueName, async (receivedMessage) =>
            {
                var parts = receivedMessage.Split(',');

                string toAddress = parts[0];
                decimal amountToSend = Convert.ToDecimal(parts[1]);

                Console.WriteLine(" [x] Received {0}", receivedMessage);

                string transactionHash = await ethereumService.TransferUSDT(toAddress, amountToSend, privateKey);
                Console.WriteLine($"Transaction Hash: {transactionHash}");
            });

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }
    }
}
