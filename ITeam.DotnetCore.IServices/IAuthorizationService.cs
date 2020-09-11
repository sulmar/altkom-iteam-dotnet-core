using ITeam.DotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITeam.DotnetCore.IServices
{
    public interface IAuthorizationService
    {
        bool TryAuthenticate(string username, string hashedPassword, out Customer customer);
    }
}
