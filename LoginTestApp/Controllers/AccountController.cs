using System;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Security;
using LoginTestApp.Business.Contracts.BusinessOperation;
using LoginTestApp.Business.Contracts.Managers;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Common;
using LoginTestApp.Crosscutting.Contracts;

namespace LoginTestApp.Controllers
{
	[AllowAnonymous]
	public class AccountController : ControllerBase
	{
		private readonly IAccountManager accountManager;
		private readonly ILogger logger;

        public AccountController(IAccountManager accountManager, ILogger logger)
		{
			this.accountManager = accountManager;
			this.logger = logger;
		}

        [DisplayName()]
		public ActionResult Index()
		{
			return View("~/Views/Shared/Login.cshtml");
		}

        [HttpGet]
        public ActionResult Login(string alias, string password)
        {
            try
            {
                if (accountManager.IsValidLogin(alias, password))
                {
                    FormsAuthentication.SetAuthCookie(alias, false);

                    return GetJson(new
                    {
                        isValid = true,
                        redirectUrl = Url.Action("Index", "Home")
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                throw;
            }

            return Json(new { isValid = false }, JsonRequestBehavior.AllowGet); ;
        }

        //[HttpGet]
        //public ActionResult Login(LoginViewModel viewModel)
        //{
        //    return Json(new { isValid = false }, JsonRequestBehavior.AllowGet); ;
        //}

        [HttpGet]
		public ActionResult Logout()
		{
			try
			{
				FormsAuthentication.SignOut();
				return RedirectToAction("Index", "Login");
			}
			catch (Exception ex)
			{
				logger.LogException(ex);
				throw;
			}
		}

		[HttpGet]
		public ActionResult PasswordRecoveryRequest(string alias, string recoveryOption)
		{
			try
			{
                BusinessOperationResult<bool> result = accountManager.PasswordRecovery(alias, recoveryOption);

				if (result.IsError)
				{
					return new GenericStateResult(result.Messages);
				}
			}
			catch (Exception ex)
			{
				logger.LogException(ex);
				throw;
			}

			return new GenericStateResult();
		}

		[HttpGet]
		public ActionResult PasswordRecovery(Guid guidId)
		{
			try
			{
			    BusinessOperationResult<bool> result = accountManager.ValidatePasswordRecoveryRequest(guidId);

                if (result.IsError)
                {
                    return new GenericStateResult(result.Messages);
                }
            }
			catch (Exception ex)
			{
				logger.LogException(ex);
				throw;
			}

            return View("~/Views/Shared/Login.cshtml", new { passwordRecoveryGuidId = guidId });
        }

	    [HttpPost]
	    public ActionResult CreateNewAccount(User newAccount)
	    {
            //TODO(AngelM): Check if it worth to expose a Web API method
	        newAccount.IsActive = true;

	        BusinessOperationResult<bool> result = accountManager.CreateNew(newAccount);

            if (result.IsError)
            {
                return new GenericStateResult(result.Messages);
            }

            return new GenericStateResult();
        }

        [HttpPost]
        public ActionResult UpdateAccount(User newAccount)
        {
            //TODO(AngelM): Check if it worth to expose a Web API method
            BusinessOperationResult<bool> result = accountManager.CreateNew(newAccount);

            if (result.IsError)
            {
                return new GenericStateResult(result.Messages);
            }

            return new GenericStateResult();
        }
    }
}
