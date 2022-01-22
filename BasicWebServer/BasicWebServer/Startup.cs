﻿using BasicWebServer.Demo.Controllers;
using BasicWebServer.Server;
using BasicWebServer.Server.Contracts;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.Cookies;
using BasicWebServer.Server.Session;
using System.Text;
using System.Web;

public static class Startup
{
    private const string LoginForm = @"<form action='/Login' method='POST'>
   Username: <input type='text' name='Username'/>
   Password: <input type='text' name='Password'/>
   <input type='submit' value ='Log In' /> 
</form>";


    private const string FileName = "content.txt";

    private const string Username = "user";
    private const string Password = "user123";

    public static async Task Main()
    {

        var server = new HttpServer(routes => routes
        .MapGet<HomeController>("/", c => c.Index())
        .MapGet<HomeController>("/Redirect", c => c.Redirect())
        .MapGet<HomeController>("/HTML", c => c.Html())
        .MapPost<HomeController>("/HTML", c => c.HtmlFormPost())
        .MapGet<HomeController>("/Content", c => c.Content())
        .MapPost<HomeController>("/Content", c => c.DownloadContent())
        .MapGet<HomeController>("/Cookies", c => c.Cookies())
        .MapGet<HomeController>("/Session", c => c.Session())
        //.MapGet<HomeController>("/Login", new HtmlResponse(LoginForm))
        //.MapPost<HomeController>("/Login", new HtmlResponse("", LoginAction))
        //.MapGet<HomeController>("/Logout", new HtmlResponse("", LogoutAction))
        //.MapGet<HomeController>("/UserProfile", new HtmlResponse("", GetUserDataAction))

        );
        await server.Start();
    }

    static void AddFromDataAction(IRequest request, IResponse response)
    {
        response.Body = string.Empty;
        foreach (var (name, value) in request.Form)
        {
            response.Body += $"{name} - {value}";
            response.Body += Environment.NewLine;
        }

    }

    public delegate int Comparison<in T>(string left, string right);

    private static void LoginAction(IRequest request, IResponse response)
    {
        request.HttpSession.Clear();

        var bodyText = "";

        bool usernameMatches = request.Form["Username"] == Username;
        bool passwordMatches = request.Form["Password"] == Password;

        if (usernameMatches && passwordMatches)
        {
            request.HttpSession[HttpSession.SessionUserKey] = "MyUserId";
            response.Cookies
                .Add(HttpSession.SessionCookieName,
                    request.HttpSession.Id);
            bodyText = "<h3>Logged successfully!</h3>";
        }
        else
        {
            bodyText = LoginForm;
        }
        response.Body = String.Empty;
        response.Body += bodyText;
    }

    private static void LogoutAction(IRequest request, IResponse response)
    {
        request.HttpSession.Clear();
        response.Body = "";
        response.Body += "<h3>Logged out successfully!</h3>";
    }

    private static void GetUserDataAction(IRequest request, IResponse response)
    {
        if (request.HttpSession.ContainsKey(HttpSession.SessionUserKey))
        {
            response.Body = "";
            response.Body += "Currently logged-in user " +
                $"is with username '{Username}'</h3>";
        }
        else
        {
            response.Body = "";
            response.Body += "<h3>You should first log in " +
                "- <a href='/Login'>Login</a></h3>";
        }
    }
}