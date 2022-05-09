using Core.Unilities;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IdentityResult> Register(UserForRegisterDto userForRegisterDto);
        Task<SignInResult> LogIn(UserForLoginDto userForLoginDto);
    
    }
}
