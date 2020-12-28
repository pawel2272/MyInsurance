using System;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface ILoginService : IDisposable
    {
        bool Login(string username, string password);
    }
}
