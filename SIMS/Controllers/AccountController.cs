using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.Models;
using SIMS.BL;

using System.Web.Security;

namespace SIMS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult Login(LoginModel loginModel, string returnUrl = null)
        {
            loginModel.ReturnUrl = returnUrl;
            return View(loginModel);
        }

        [HttpPost]
        //[AllowAnonymous]
        //    [ValidateAntiForgeryToken]     
        public ActionResult Login(LoginModel loginModel, int remember = 0)
        {

            if (ModelState.IsValid)
            {

                bool rememberMe = true;
                Users users = Users.RetrieveByEmail(loginModel.Email.Trim(), loginModel.Password.Trim());

                if (users != null)
                {
                    String verifiedRoles = string.Empty;
                    if (users.Usertype.Trim() == "Group Head")
                    {
                        verifiedRoles = "Group Head";

                    }
                    else if (users.Usertype.Trim() == "Executive Operations")
                    {
                        verifiedRoles = "Executive Operations";

                    }
                    else if (users.Usertype.Trim() == "Executive Marketing")
                    {
                        verifiedRoles = "Executive Marketing";

                    }
                    else if (users.Usertype.Trim() == "Executive Accounts")
                    {
                        verifiedRoles = "Executive Accounts";

                    }
                    else if (users.Usertype.Trim() == "Associate Admin")
                    {
                        verifiedRoles = "Associate Admin";

                    }
                    else if (users.Usertype.Trim() == "Associate Operations")
                    {
                        verifiedRoles = "Associate Operations";

                    }
                    else if (users.Usertype.Trim() == "Associate Accounts")
                    {
                        verifiedRoles = "Associate Accounts";

                    }
                    else if (users.Usertype.Trim() == "Associate Marketing")
                    {
                        verifiedRoles = "Associate Marketing";

                    }
                    else if (users.Usertype.Trim() == "Admin")
                    {
                        verifiedRoles = "Admin";
                    }
                    //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, loginModel.Email.Trim(), DateTime.Now, DateTime.Now.AddDays(1), rememberMe, verifiedRoles, FormsAuthentication.FormsCookiePath);
                    //string hash = FormsAuthentication.Encrypt(ticket);
                    //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    //cookie.Expires = DateTime.Now.AddDays(1);
                    //Response.Cookies.Add(cookie);


                    HttpCookie authCookie = FormsAuthentication.GetAuthCookie(loginModel.Email.Trim(), false);

                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    DateTime expDate = ticket.Expiration;

                    expDate = DateTime.Now.AddDays(1);

                    FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, expDate, ticket.IsPersistent, verifiedRoles);


                    authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                    authCookie.Expires = expDate;

                    Response.Cookies.Add(authCookie);

                    if (loginModel.ReturnUrl != null)
                    {
                        return Redirect(loginModel.ReturnUrl);
                    }
                    else
                    {

                        return RedirectToAction("Index", "Staff");
                    }
                }

            }


            // If we got this far, something failed, redisplay form
            TempData["ErrorMsg"] = "Email or password incorrect.";

            //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(loginModel);

        }

        private ActionResult Redirect(object siteBaseAddress, string v)
        {
            throw new NotImplementedException();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Staff");
        }
    }
}