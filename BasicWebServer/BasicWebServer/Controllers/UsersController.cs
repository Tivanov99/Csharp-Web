﻿namespace BasicWebServer.Demo.Controllers
{
    using BasicWebServer.Server.Contracts;
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.Cookies;
    using BasicWebServer.Server.Session;

    public class UsersController : Controller
    {
        private const string Username = "user";
        private const string Password = "user123";

        public UsersController(IRequest request)
            : base(request)
        {
        }

        public IResponse Login()
            => View();

        public IResponse LogInUser()
        {
            this.Request.HttpSession.Clear();

            bool usernameMatches = this.Request.Form["Username"] == Username;
            bool passwordMatches = this.Request.Form["Password"] == Password;

            if (usernameMatches && passwordMatches)
            {
                if (!this.Request.HttpSession
                    .ContainsKey(HttpSession.SessionUserKey))
                {
                    this.Request.HttpSession[HttpSession.SessionUserKey] = "MyUserId";

                    var cookies = new CookieCollection();
                    cookies.Add(HttpSession.SessionCookieName,
                        this.Request.HttpSession.Id);

                    return Html("<h3>Logged successfully!</h3>", cookies);
                }
                return Html("<h3>Logged successfully!</h3>");
            }
            return Redirect("/Login");
        }

        public IResponse LogOut()
        {
            this.Request.HttpSession.Clear();
            return Html("<h3>Logged out successfully!</h3>");
        }

        public IResponse Register()
        {
            return this.View();
        }

        public IResponse Registration()
        {
            string email = this.Request.Form["Email"];
            string pass = this.Request.Form["Psw"];
            string passRepeat = this.Request.Form["Psw-Repeat"];

            if (pass == passRepeat && !string.IsNullOrEmpty(email))
            {

            }
            return this.View();
        }

        public IResponse UserProfile()
        {
            if (this.Request.HttpSession
                .ContainsKey(HttpSession.SessionUserKey))
            {
                return Html("Currently logged-in user " +
                     $"is with username '{Username}'</h3>");
            }

            return Html("<h3>You should first log in " +
               "- <a href='/Login'>Login</a></h3>");
        }
    }
}
