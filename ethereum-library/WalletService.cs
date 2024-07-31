using NBitcoin;
using Nethereum.HdWallet;
using Nethereum.Web3.Accounts;
using EthereumLibrary.Abstraction;

namespace EthereumLibrary
{
    public class WalletService: IWalletService
    {
        public Wallet CreateWallet(string password)
        {
            var wallet = new Wallet(Wordlist.English, WordCount.Twelve);
            return wallet;
        }

        public Account GetAccountFromMnemonic(string mnemonic, int accountIndex = 0)
        {
            var wallet = new Wallet(mnemonic, null);
            return wallet.GetAccount(accountIndex);
        }
    }
 
}
