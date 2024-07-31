using Nethereum.HdWallet;
using Nethereum.Web3.Accounts;

namespace EthereumLibrary.Abstraction
{
    public interface IWalletService
    {
        Wallet CreateWallet(string password);
        Account GetAccountFromMnemonic(string mnemonic, int accountIndex = 0);
    }
}
