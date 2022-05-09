using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using DataAccess.Abstract;
using Core.Unilities;
using Business.Constants;

//using Identity.Models;


namespace Business.Concrete
{
    public class UserForManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        public UserForManager(IUserDal userDal, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _userDal = userDal;
            _signInManager= signInManager;

        }

   

        public async Task <IdentityResult> Register(UserForRegisterDto userForRegisterDto)
        {
            AppUser appUser = new AppUser();
 
            appUser.UserName = userForRegisterDto.UserName;
            appUser.FirstName = userForRegisterDto.FirstName;
            appUser.LastName = userForRegisterDto.LastName;
            appUser.BirthPlace = userForRegisterDto.BirthPlace;
            appUser.BirthDate = userForRegisterDto.BirthDate;
            appUser.Email = userForRegisterDto.EMail;
         IdentityResult result=   await _userManager.CreateAsync(appUser,userForRegisterDto.Password);
          return result;
        }

    

        public async Task<SignInResult> LogIn(UserForLoginDto userForLoginDto)
        {
            if (userForLoginDto.EMail!=null)
            {
                AppUser user = await _userManager.FindByEmailAsync(userForLoginDto.EMail);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                }
                SignInResult result = _signInManager.PasswordSignInAsync(user, userForLoginDto.Password, userForLoginDto.RememberMe, false).Result;
                return result;
            }
          

            return null;
   
        
            
            

           

        }


    }
}
