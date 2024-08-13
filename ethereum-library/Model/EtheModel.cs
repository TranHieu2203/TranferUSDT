using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace ethereum_library.Model
{
    public class Log
    {
        public string address { get; set; }
        public List<string> topics { get; set; }
        public string data { get; set; }
        public string blockNumber { get; set; }
        public string transactionHash { get; set; }
        public string transactionIndex { get; set; }
        public string blockHash { get; set; }
        public string logIndex { get; set; }
        public bool removed { get; set; }
    }

    public class TransactionInfo
    {
        public string transactionHash { get; set; }
        public string transactionIndex { get; set; }
        public string blockHash { get; set; }
        public string blockNumber { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string cumulativeGasUsed { get; set; }
        public string gasUsed { get; set; }
        public string effectiveGasPrice { get; set; }
        public object contractAddress { get; set; }
        public string status { get; set; }
        public List<Log> logs { get; set; }
        public string type { get; set; }
        public string logsBloom { get; set; }
        public object root { get; set; }
    }
    public class TransactionDTO
    {
        public string transactionHash { get; set; }
        public string from { set; get; }
        public List<string> to { set; get; }
        public BigInteger value { get; set; }
        public int status { set; get; }
    }
}
