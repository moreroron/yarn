using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using yarn_rider.Models;

namespace yarn_rider.Controllers
{
    public class AccountController : Controller
    {
        // GET Registration
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        // POST Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")]
            User user)
        {
            bool Status = false;
            string message = "";

            // Model Validation 
            if (ModelState.IsValid)
            {
                // Generate Random Avatar
                Random rnd = new Random();
                int rndNum = rnd.Next(0, 5);
                user.Avatar = "/Images/profileIcons/" + rndNum + ".svg";


                #region //Email is already Exist 

                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }

                #endregion

                #region Generate Activation Code 

                user.ActivationCode = Guid.NewGuid();

                #endregion

                #region  Password Hashing 

                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword); //

                #endregion

                user.IsEmailVerified = false;

                #region Save to Database

                using (SiteDbContext dc = new SiteDbContext())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();

                    //Send Email to User
                    SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                    message = "Registration successfully done. Account activation link" +
                              " has been sent to your email: " + user.EmailID;
                    Status = true;
                }

                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }


        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (SiteDbContext dc = new SiteDbContext())
            {
                var v = dc.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }


        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            //MailMessage msg = new MailMessage();
            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            //    msg.Subject = "Add Subject";
            //    msg.Body = "Add Email Body Part";
            //    msg.From = new MailAddress("yarnEmailator@gmail.com");
            //    msg.To.Add("rnmo90@gmail.com");
            //    msg.IsBodyHtml = true;
            //    client.Host = "smtp.gmail.com";
            //    System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("yarnemailator@gmail.com", "yarnEmailator1111");
            //    client.Port = int.Parse("587");
            //    client.EnableSsl = true;
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = basicauthenticationinfo;
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client.Send(msg);
            //}

            var verifyUrl = "/Account/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("yarnemailator@gmail.com", null);
            var toEmail = new MailAddress(emailID, null);
            var fromEmailPassword = "yarnEmailator1111";
            string subject = "Your account is successfully created!";
            string body = "<br/><br/>We are excited to tell you that your yarn account is" +
                          " successfully created. Please click on the below link to verify your account" +
                          " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }


        // GET VerifyAccount
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (SiteDbContext dc = new SiteDbContext())
            {
                dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                // Confirm password does not match issue on save changes
                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }

            ViewBag.Status = Status;
            return View();
        }

        
        public ActionResult Login()
        {
//            if (Session["currentUserEmailID"] != null)
//            {
//                return RedirectToAction("About", "Home");
//            }

            return View();
            
        }

        [HttpPost]
        public ActionResult Login(UserLogin login)
        {
            string message = "";
            SiteDbContext db = new SiteDbContext();

            var currentUser = db.Users.FirstOrDefault(u => u.EmailID.Equals(login.EmailID));

            if (currentUser != null)
            {
                if (!currentUser.IsEmailVerified)
                {
                    message = "Please verify your email first";
                    ViewBag.Message = message;
                    return View();
                }

                if (String.CompareOrdinal(Crypto.Hash(login.Password), currentUser.Password) == 0)
                {
                    
                    
                    
                    Session["currentUser"] = currentUser;
                    message = "Welcome, " + login.EmailID + ".";
                }
                
                else
                {
                    message = "Invalid credential provided";
                    ViewBag.Message = message;
                    return View();
                }
            }

            else
            {
                ViewBag.message = "No such user found";
                return View();
            }


            ViewBag.Message = message;
            return RedirectToAction("Index", "Home");
        }



        public ActionResult Logout()
        {

            Session["currentUser"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}