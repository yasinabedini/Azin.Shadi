using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Azin.Shadi.Web.Areas.UserPanel.Controllers;

[Area("Userpanel")]
[Authorize]
public class WalletController : Controller
{
    private readonly IUserService _userService;
    private readonly IPermissionService _permissionService;

    public WalletController(IUserService userService, IPermissionService permissionService)
    {
        this._userService = userService;
        _permissionService = permissionService;
    }

    [Route("/Userpanel/WalletRecharge")]
    public IActionResult WalletRecharge()
    {
        string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
        ViewBag.TransactionList = _userService.GetUserWalletTransaction(username);
        return View();
    }

    [Route("/Userpanel/WalletRecharge")]
    [HttpPost]
    public IActionResult WalletRecharge(WalletRechargeViewModel Transaction)
    {
        string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
        if (!ModelState.IsValid)
        {

            ViewBag.TransactionList = _userService.GetUserWalletTransaction(username);
            return View(Transaction);
        }

        int transactionId = _userService.AddTransaction(username, Transaction.Amount, "شارژ کیف پول", 3);

        var payment = new ZarinpalSandbox.Payment(Transaction.Amount);

        var res = payment.PaymentRequest(_userService.GetTransactionById(transactionId).Description, "https://Azinshadi.ir/onlinepayment/" + transactionId, "yasinabedini1384@gmail.com", "09106966244");

        if (res.Result.Status == 100)
        {
            Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
        }


        return View();
    }
}

