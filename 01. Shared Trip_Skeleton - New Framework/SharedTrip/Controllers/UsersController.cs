﻿namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using MyWebServer.Services;
    using SharedTrip.ApplicationModels;
    using SharedTrip.AppServices;
    using SharedTrip.Data;
    using SharedTrip.Models;
    using SharedTrip.Validator;
    using System.Collections.Generic;
    using System.Linq;

    public class UsersController : Controller
    {
        private ApplicationDbContext dbContext;
        private IUserService userService;

        public UsersController(ApplicationDbContext context,
            IUserService userService)
        {
            this.dbContext = context;
            this.userService = userService;
        }
        public HttpResponse Login()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.View();
            }
            return this.Error("You can't access this page because you are logged in your account!");
        }

        [HttpPost]
        public HttpResponse Login(UserLoginForm userLoginForm)
        {
            string hashedPassword = this.passwordHasher
                .HashPassword(userLoginForm.Password);

            var userId = this.dbContext
                .Users
                .Where(x => x.Username == userLoginForm.Username &&
                x.Password == hashedPassword)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (userId != null)
            {
                this.SignIn(userId);
                return this.Redirect("/Trips/All");
            }

            return this.View();
        }

        public HttpResponse Register()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.View();
            }
            return this.Error(new List<string>() { "You can't access this page because you are logged in your account!" });
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterForm userRegisterForm)
        {
            if (this.userDataValidator
                .IsValidRegistraionData(userRegisterForm.Username,
                userRegisterForm.Email,
                userRegisterForm.Password,
                userRegisterForm.ConfirmPassword))
            {
                string hashedPassword = this.passwordHasher
                    .HashPassword(userRegisterForm.Password);

                User user = new()
                {
                    Username = userRegisterForm.Username,
                    Email = userRegisterForm.Email,
                    Password = hashedPassword
                };

                this.dbContext.Users.Add(user);
                this.dbContext.SaveChanges();
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
