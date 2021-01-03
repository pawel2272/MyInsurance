using MyInsurance.BusinessLogic.Interfaces;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;

namespace MyInsurance.BusinessLogic.Services
{
    public class LoginService<DTO, SERVICE> : ILoginService
        where DTO : ILoginable
        where SERVICE : IPerson
    {
        private readonly SERVICE service;
        private readonly CryptoService crypto;
        private DTO person;
        private bool isLogged = false;

        private bool SetLoggedIn()
        {
            isLogged = true;
            return true;
        }

        private bool SetLoggedOut()
        {
            isLogged = false;
            return false;
        }

        public DTO GetLoggedPerson
        {
            get
            {
                if (isLogged)
                {
                    if (person != null)
                        return person;
                }
                throw new NullReferenceException();
            }
        }

        public LoginService(SERVICE service, CryptoService crypto)
        {
            this.service = service;
            this.crypto = crypto;
        }

        public bool Login(string username, string password)
        {
            person = (DTO)service.GetPerson(username);
            if (person != null)
            {
                string encryptedPassword = crypto.Encrypt(password);
                if (encryptedPassword.Equals(person.Password))
                {
                    return SetLoggedIn();
                }
            }
            return SetLoggedOut();
        }

        public void Dispose()
        {
            ((IDisposable)service).Dispose();
            crypto.Dispose();
        }
    }
}
