using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;
using Nethereum.Contracts;
using EthereumLibrary.Abstraction;
using Nethereum.Hex.HexTypes;
using Nethereum.Util;
using Nethereum.RPC.TransactionReceipts;
using Nethereum.RPC.Eth.DTOs;

namespace EthereumLibrary
{
    public class EthereumService: IEthereumService
    {
        private readonly string _infuraUrl;
        private readonly string _contractAddress;

        public EthereumService(string infuraUrl, string contractAddress)
        {
            _infuraUrl = infuraUrl;
            _contractAddress = contractAddress;
        }

        public async Task<string> TransferUSDT(string toAddress, decimal amountToSend, string privateKey)
        {
            try
            {
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


                var contract = web3.Eth.GetContract(contractAbi, contractAddress);
                var transferFunction = contract.GetFunction("transfer");

                // Kiểm tra số dư BNB của tài khoản
                var balance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);
                var balanceInBNB = UnitConversion.Convert.FromWei(balance.Value);

                // Chuyển đổi số lượng USDT thành đơn vị nhỏ hơn
                var decimals = 18; // Số chữ số thập phân của USDT trên BSC
                var amountToSendInWei = UnitConversion.Convert.ToWei(amountToSend, decimals);

                // Lấy giá gas hiện tại
                var currentGasPrice = await web3.Eth.GasPrice.SendRequestAsync();
                var currentGasPriceInWei = currentGasPrice.Value;
                var currentGasPriceInGwei = UnitConversion.Convert.FromWei(currentGasPriceInWei, UnitConversion.EthUnit.Gwei);


                // Tăng giá gas để đảm bảo giao dịch được 
                decimal gasPriceIncreasePercentage = 0.5m; // Tăng giá gas thêm 50%
                decimal increasedGasPriceInGwei = currentGasPriceInGwei * (1 + gasPriceIncreasePercentage);
                var increasedGasPriceInWei = UnitConversion.Convert.ToWei(increasedGasPriceInGwei, UnitConversion.EthUnit.Gwei);


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

                    // Chờ biên nhận giao dịch
                    var receiptService = new TransactionReceiptPollingService(web3.TransactionManager);
                    var transactionReceipt = await receiptService.PollForReceiptAsync(transactionHash);
                    return transactionReceipt.BlockHash;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }


            }
            catch (Exception ex)
            {

                return ex.Message;
            }
         
        }

        // Lấy thông tin giao dịch byHash
        public async Task<TransactionReceipt> GetTransactionReceipt(string transactionHash)
        {
            string nodeUrl = "https://bsc-dataseed.binance.org/";

            var web3 = new Web3(nodeUrl);
            return await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
        }

        // check số dư
        public async Task<decimal> GetBalance(string address)
        {
        var balanceFunction = new BalanceOfFunction
        {
            Owner = address
        };
            var web3 = new Web3(_infuraUrl);

            var contract = web3.Eth.GetContract(_usdtAbi, address);
        var balanceHandler = contract.GetFunction("balanceOf");

        var balance = await balanceHandler.CallAsync<BigInteger>(balanceFunction);
        return Web3.Convert.FromWei(balance, 6); // Chuyển đổi từ Wei sang USDT (6 chữ số thập phân)
        }

        public Task<decimal> GetBalance(string address, string privateKey)
        {
            throw new NotImplementedException();
        }

        public class BalanceOfFunction
        {
            public string Owner { get; set; }
        }

        private const string _usdtAbi = @"[
        {
            'constant': true,
            'inputs': [{'name': 'owner', 'type': 'address'}],
            'name': 'balanceOf',
            'outputs': [{'name': 'balance', 'type': 'uint256'}],
            'payable': false,
            'stateMutability': 'view',
            'type': 'function'
        }
    ]";


        [Function("transfer", "bool")]
        public class TransferFunction : FunctionMessage
        {
            [Parameter("address", "_to", 1)]
            public string To { get; set; }

            [Parameter("uint256", "_value", 2)]
            public BigInteger TokenAmount { get; set; }
        }
    }
}
