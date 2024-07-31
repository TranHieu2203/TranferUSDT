using EthereumLibrary.Abstraction;
using LoggingLibrary;
using Nethereum.Hex.HexTypes;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using RabbitMqLib;
using System.Numerics;
using Nethereum.Util;
using Nethereum.RPC.TransactionReceipts;

namespace TranferUSDT
{
    public partial class Form1 : Form
    {
        private readonly IEthereumService _ethereumService;
        private readonly IWalletService _walletService;
        public Form1(
            IEthereumService ethereumService, IWalletService walletService

                     )
        {
            InitializeComponent();
            _ethereumService = ethereumService;
            _walletService = walletService;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.LogInfo("Load demo form.");
            txtPrivateKey.Text = "0x2c1091b486d3a0533afd0672a71f07189e74168be8a2404d1d13aade4ce9699c";

        }
        private Nethereum.Web3.Accounts.Account CreateWallet()
        {
            var wallet = _walletService.CreateWallet("123456");
            var account = wallet.GetAccount(0);

            txtLog.Text += $"Mnemonic: {string.Join(',', wallet.Words)}";
            // xuống dòng
            txtLog.Text += Environment.NewLine;
            txtLog.Text += $"Address: {account.Address}";
            txtLog.Text += Environment.NewLine;
            txtLog.Text += $"Private Key: {account.PrivateKey}";
            txtLog.Text += Environment.NewLine;
            txtLog.Text += "------------";

            // ghi vào file log các thông tin
            Logger.LogInfo("------------");
            Logger.LogInfo($"Mnemonic: {string.Join(',', wallet.Words)}");
            Logger.LogInfo($"Address: {account.Address}");
            Logger.LogInfo($"Private Key: {account.PrivateKey}");
            Logger.LogInfo("------------");
            return account;

        }
        private void btnCreateWallet_Click(object sender, EventArgs e)
        {
            this.CreateWallet();
        }

        private async void btnCreateAndTranferUsdt_Click(object sender, EventArgs e)
        {
            string rabbitMqHost = "localhost";
            var account = this.CreateWallet();
            var privateKey = txtPrivateKey.Text;
            var rabbitMqService = new RabbitMqService(rabbitMqHost);
            string queueName = "usdt_transfer_queue";

            // chuyenr tiền từ ví 1 vào ví mới tạo
            var toAdress = account.Address;
            var amountToSend = 10;
            var result = await _ethereumService.TransferUSDT(toAdress, amountToSend, privateKey);
            txtLog.Text += $"Transaction Hash: {result}";
            // Địa chỉ ví của admin, đang mặc định địa chỉ của béo lồn
            var toAdress2 = "0x936F604a425583A042b0a37873B4D369D2D00893";
            var amountToSend2 = 10;
            var result2 = await _ethereumService.TransferUSDT(toAdress2, amountToSend2, account.PrivateKey);
            txtLog.Text += $"Transaction Hash: {result2}";

            //// Gửi yêu cầu chuyển USDT vào hàng đợi
            //string message = $"{account.Address},10.0"; // Giả sử gửi 10 USDT
            //rabbitMqService.SendMessage(queueName, message);

            //// Nhận và xử lý yêu cầu từ hàng đợi
            //rabbitMqService.ReceiveMessage(queueName, async (receivedMessage) =>
            //{
            //    var parts = receivedMessage.Split(',');

            //    string toAddress = parts[0];
            //    decimal amountToSend = Convert.ToDecimal(parts[1]);

            //    txtLog.Text += (" [x] Received {0}", receivedMessage);

            //    string transactionHash = await this._ethereumService.TransferUSDT(toAddress, amountToSend, privateKey);
            //    txtLog.Text += ($"Transaction Hash: {transactionHash}");
            //});

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public async void btnCheck_Click(object sender, EventArgs e)
        {
        }

        private async void btnSendToBeoLon_Click(object sender, EventArgs e)
        {
            try
            {



                string toAddress = "0x936F604a425583A042b0a37873B4D369D2D00893"; // Địa chỉ người nhận
                decimal amountToSend = 9; // Số lượng USDT cần gửi
                string privateKey = "2c1091b486d3a0533afd0672a71f07189e74168be8a2404d1d13aade4ce9699c"; // Khóa bí mật (không cần 0x)
                string bscRpcUrl = "https://bsc-dataseed2.binance.org/"; // URL RPC của BSC

                var account = new Account(privateKey);
                var web3 = new Web3(account, bscRpcUrl);

                // Địa chỉ hợp đồng USDT trên BSC
                string contractAddress = "0x55d398326f99059ff775485246999027b3197955";

                // ABI của hợp đồng USDT
                var contractAbi = @"[
            {
                ""constant"": false,
                ""inputs"": [
                    {
                        ""name"": ""_to"",
                        ""type"": ""address""
                    },
                    {
                        ""name"": ""_value"",
                        ""type"": ""uint256""
                    }
                ],
                ""name"": ""transfer"",
                ""outputs"": [
                    {
                        ""name"": """",
                        ""type"": ""bool""
                    }
                ],
                ""payable"": false,
                ""stateMutability"": ""nonpayable"",
                ""type"": ""function""
            }
            ]";
                var blockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
                txtLog.Text +=("Connected successfully. Current block number: " + blockNumber.Value) + "\n";


                var contract = web3.Eth.GetContract(contractAbi, contractAddress);
                var transferFunction = contract.GetFunction("transfer");

                // Kiểm tra số dư BNB của tài khoản
                var balance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);
                var balanceInBNB = UnitConversion.Convert.FromWei(balance.Value);
                txtLog.Text += "Balance in BNB: " + balanceInBNB + Environment.NewLine;

                // Chuyển đổi số lượng USDT thành đơn vị nhỏ hơn
                var decimals = 18; // Số chữ số thập phân của USDT trên BSC
                var amountToSendInWei = UnitConversion.Convert.ToWei(amountToSend, decimals);

                // Lấy giá gas hiện tại
                var currentGasPrice = await web3.Eth.GasPrice.SendRequestAsync();
                var currentGasPriceInWei = currentGasPrice.Value;
                var currentGasPriceInGwei = UnitConversion.Convert.FromWei(currentGasPriceInWei, UnitConversion.EthUnit.Gwei);

                txtLog.Text += "Current Gas Price (in Wei): " + currentGasPriceInWei + Environment.NewLine;
                txtLog.Text += "Current Gas Price (in Gwei): " + currentGasPriceInGwei + Environment.NewLine;

                // Tăng giá gas để đảm bảo giao dịch được khai thác
                decimal gasPriceIncreasePercentage = 0.5m; // Tăng giá gas thêm 50%
                decimal increasedGasPriceInGwei = currentGasPriceInGwei * (1 + gasPriceIncreasePercentage);
                var increasedGasPriceInWei = UnitConversion.Convert.ToWei(increasedGasPriceInGwei, UnitConversion.EthUnit.Gwei);

                txtLog.Text += "Increased Gas Price (in Wei): " + increasedGasPriceInWei + Environment.NewLine;
                txtLog.Text += "Increased Gas Price (in Gwei): " + increasedGasPriceInGwei + Environment.NewLine;

                var gasLimit = new HexBigInteger(60000); // Giới hạn gas

                // Tạo giao dịch
                var transactionInput = transferFunction.CreateTransactionInput(
                    account.Address, // Địa chỉ gửi
                    gasLimit, // Giới hạn gas
                    new HexBigInteger(increasedGasPriceInWei), // Giá gas
                    null, // Giá trị (value) trong giao dịch
                    toAddress, // Địa chỉ người nhận
                    amountToSendInWei // Số lượng USDT gửi
                );

                // Ký và gửi giao dịch
                try
                {
                    var transactionHash = await web3.Eth.TransactionManager.SendTransactionAsync(transactionInput);
                    txtLog.Text += "Transaction hash: " + transactionHash + Environment.NewLine;

                    // Chờ biên nhận giao dịch
                    var receiptService = new TransactionReceiptPollingService(web3.TransactionManager);
                    var transactionReceipt = await receiptService.PollForReceiptAsync(transactionHash);
                    txtLog.Text += "Transaction was mined in block: " + transactionReceipt.BlockNumber.Value + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    txtLog.Text += "Error: " + ex.Message + Environment.NewLine;
                }




            }
            catch (Exception ex)
            {
                txtLog.Text += ex.Message;
            }
       

            //var toAdress = "0x936F604a425583A042b0a37873B4D369D2D00893";
            //var amountToSend = 1;
            //var privateKey = txtPrivateKey.Text;

            //var result =  await _ethereumService.TransferUSDT(toAdress,1,privateKey);
            //txtLog.Text += $"Transaction Hash: {result}";

        }
    }
}
