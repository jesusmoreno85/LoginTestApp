using System;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Security;
using LoginTestApp.Business.Contracts.Managers;
using LoginTestApp.Business.Contracts.Models;
using LoginTestApp.Business.Contracts.ModelValidators;
using LoginTestApp.Common;
using LoginTestApp.Crosscutting.Contracts;

namespace LoginTestApp.Controllers
{
	[AllowAnonymous]
	public class LoginController : ControllerBase
	{
		private readonly IAccountManager accountManager;
		private readonly ILogger logger;
        private readonly IUserValidator userValidator;

        public LoginController(IAccountManager accountManager, ILogger logger, IUserValidator userValidator)
		{
			this.accountManager = accountManager;
			this.logger = logger;
            this.userValidator = userValidator;
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
				string errorMessage;
				accountManager.PasswordRecovery(alias, recoveryOption, out errorMessage);

				if (!string.IsNullOrWhiteSpace(errorMessage))
				{
					return new GenericStateResult(true, errorMessage);
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
				string errorMessage;
				if (!accountManager.ValidatePasswordRecoveryRequest(guidId, out errorMessage))
				{
					return new GenericStateResult(true, errorMessage);
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
            //TODO(AngelM): Move this logic to business layer
            var asdasd = userValidator.IsValidForCreate(newAccount);

            //asdasd = userValidator.Validate(newAccount, ruleSet: UserValidator.CreateNewValidation2);

            if (!ModelState.IsValid)
            { 
                newAccount.IsActive = true;

                accountManager.CreateNew(newAccount);

                newAccount.Id = 0;
                accountManager.CreateNew(newAccount);

                newAccount.Id = 0;
                accountManager.CreateNew(newAccount);

            }

            return new GenericStateResult();
        }
	}
}
