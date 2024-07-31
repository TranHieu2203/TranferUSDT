using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumLibrary.Abstraction
{
    public interface IEthereumService
    {
        Task<string> TransferUSDT(string toAddress, decimal amountToSend, string privateKey);
        Task<decimal> GetBalance(string address, string privateKey);
            }
}
