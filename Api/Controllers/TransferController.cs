using EthereumLibrary.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {

        private readonly IEthereumService _ethereumService;
        private readonly IWalletService _walletService;


        // constructor
        public TransferController(
            IEthereumService ethereumService, IWalletService walletService)
        {
            _ethereumService = ethereumService;
            _walletService = walletService;
        }
        [HttpPost("send-usdt")]
        public async Task<IActionResult> SendUsdt(TransferUsdt transferUsdt)
        {
            var receipt = await _ethereumService.TransferUSDT(transferUsdt.ToAdress, transferUsdt.AmountToSend, transferUsdt.PrivateKey);

            return Ok($"Transaction Hash: {receipt}");
        }

        // CreateWallet 
        [HttpPost("create-wallet")]
        public IActionResult CreateWallet()
        {
            var wallet = _walletService.CreateWallet("123456");
            var account = wallet.GetAccount(0);
            // ghi vào file log các thông tin
            LoggingLibrary.Logger.LogInfo("------------");
            LoggingLibrary.Logger.LogInfo($"Mnemonic: {string.Join(',', wallet.Words)}");
            LoggingLibrary.Logger.LogInfo($"Address: {account.Address}");
            LoggingLibrary.Logger.LogInfo($"Private Key: {account.PrivateKey}");
            LoggingLibrary.Logger.LogInfo("------------");
            return Ok(account);
        }
    }
}
