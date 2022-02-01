﻿namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Models;

    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            if (this.User.IsAuthenticated)
            {
                return this.NotFound();
            }
            return this.View();
        }

        public HttpResponse Register()
        {
            if (this.User.IsAuthenticated)
            {
                return this.NotFound();
            }
            return this.View();
        }

        public HttpResponse LoggingInUser()
        {
            bool validUsername = this.Request.Form["username"] == "user";
            bool validPassword = this.Request.Form["password"] == "user123";

            if (validUsername && validPassword)
            {
                this.SignIn(this.Request.Session.Id);
                return this.Redirect("/Index");
            }

            return this.Login();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel registerForm)
        {
            string username = this.Request.Form["username"];
            string password = this.Request.Form["password"];
            string email = this.Request.Form["email"];
            string confirmPassword = this.Request.Form["confirmPassword"];

            if (password == confirmPassword)
            {
                //TODO: Add new user to the db
                return this.Login();
            }

            return this.Register();
        }
        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/Index");
        }
    }
}
