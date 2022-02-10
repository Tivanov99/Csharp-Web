﻿namespace MyWebServer.Services
{
    using MyWebServer.DataForm;
 

    public interface IUserService
    {
        bool IsUserExist(LoginDataForm loginDataForm);

        void CreateUser(RegisterDataForm registerDataForm);
    }
}
