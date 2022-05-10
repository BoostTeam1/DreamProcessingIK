    using Business.Abstract;
using DreamProcessingIK.Models;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Core.Helper;
using System.Collections.Generic;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DreamProcessingIK.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;
        private readonly IUserCompanyService _userCompanyService;
        public UserManager<AppUser> _userManager;
        public RoleManager<AppRole> _roleManager;
        public SignInManager<AppUser> _signInManager;

        public HomeController(IUserService userSevice, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, IUserCompanyService userCompanyService, ICompanyService companyService)
        {
            _roleManager = roleManager;
            _companyService = companyService;
            _userCompanyService = userCompanyService;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userSevice;
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserForRegisterDto register)
        {
            AppUser user = new AppUser();
            user.UserName = register.UserName;
            user.FirstName = register.FirstName;
            user.LastName = register.LastName;
            user.Email = register.EMail;
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string link = Url.Action("UserConfirmEmail", "Home", new
                {
                    userId = user.Id,
                    token = confirmationToken
                }, protocol: HttpContext.Request.Scheme
                );
                UserEmailConfirmationHelper.EmailConfirmationSend(link, user.Email);
            }
            else
            {
                AddModelError(result);
            }
            return View(register);
        }

        public async Task<IActionResult> UserConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, "Manager");

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.status = "Email adresiniz onaylanmıştır.Login ekranından giriş yapabilirsiniz";
            }
            else
            {
                ViewBag.status = "Bir hata meydana geldi lütfen daha sonra tekrar deneyiniz";
            }
            return View();
        }

        public IActionResult LogIn(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(UserForLoginDto userForLoginDto)
        {
            if (ModelState.IsValid)
            {
                SignInResult result = await _userService.LogIn(userForLoginDto);
                if (result.Succeeded)
                {
                    AppUser user = await _userManager.FindByEmailAsync(userForLoginDto.EMail);
                    if (_userManager.IsEmailConfirmedAsync(user).Result == false)
                    {
                        ModelState.AddModelError("", "Email Adresiniz Onaylanmamıştır.Lütfen epostanızı kontrol ediniz");
                        return View(userForLoginDto);
                    }

                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    foreach (var item in roles)
                    {
                        if (item.Contains("Manager"))
                        {
                            userForLoginDto.UserCompanyDto = _userCompanyService.GetByUserId(user.Id);
                            userForLoginDto.Company = _companyService.GetById((int)userForLoginDto.UserCompanyDto.CompanyId);
                            if (user.IsConfirmed == false)
                            {
                                ModelState.AddModelError("", "Şirketiniz henüz sistemimiz tarafından onaylanmamıştır.");
                                return View(userForLoginDto);
                            }
                            if (userForLoginDto.Company.EndDate <= System.DateTime.Now)
                            {
                                ModelState.AddModelError("", "Şirketinizin paketimize erişim süresi bitmiştir. Lütfen tekrar başvuru yapınız.");
                                return View(userForLoginDto);
                            }
                            if (TempData["ReturnUrl"] != null)
                            {
                                return Redirect(TempData["ReturnUrl"].ToString());
                            }
                            return RedirectToAction("AddEmployee", "Manager");
                        }
                        else if (item.Contains("Admin"))
                        {
                            if (TempData["ReturnUrl"] != null)
                            {
                                return Redirect(TempData["ReturnUrl"].ToString());
                            }
                            return RedirectToAction("_AdminDashboard", "Admin");
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adi veya şifresi");
                }
            }
            return View(userForLoginDto);

        }
        public IActionResult ResetPassword()
        {
            return View();
            
        }
        [HttpPost]
        public IActionResult ResetPassword(UserForPasswordResetDto forPasswordResetDto)
        {
            AppUser user = _userManager.FindByEmailAsync(forPasswordResetDto.Email).Result;
            if (user != null)
            {
                string passwordResetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                string passwordResetLink = Url.Action("ResetPasswordConfirm", "Home", new
                {
                    userId = user.Id,
                    token = passwordResetToken,
                }, HttpContext.Request.Scheme);
                MailSenderHelper.PasswordResetSendEmail(passwordResetLink, user.Email);
                ViewBag.status = "successful";


            }
            else
            {
                ModelState.AddModelError("", "Sistemde kayıtlı bir email adresi bulunanamamıştır");
            }
            return View(forPasswordResetDto);

        }
        public IActionResult ResetPasswordConfirm(string userId,string token)
        {
            TempData["userId"]=userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm([Bind("PasswordNew")] UserForPasswordResetDto userForPasswordResetDto)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, userForPasswordResetDto.PasswordNew);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    TempData["PasswordResetInfo"] = "Şifreniz başarıyla yenilenmiştir.Yeni şifreniz ile giriş yapınız";
                    ViewBag.status = "success";
                    return RedirectToAction("LogIn");
                }
                else
                {
                    AddModelError(result);
                }
            }
            ModelState.AddModelError("", "Bir hata meydana gelmiştir");
            return View(userForPasswordResetDto);
        }
        public void LogOut()
        {
            _signInManager.SignOutAsync();
        }

        public IActionResult Details(string id)
        {
            var result = _userManager.FindByIdAsync(id).Result;

            Company company = _companyService.GetById((int)_userCompanyService.GetByUserId(result.Id).CompanyId);
            CompanyCommentDetailsDto dto = new CompanyCommentDetailsDto();
            dto.CompanyName = company.CompanyName;
            dto.CompanyDetail = company.CompanyDetail;
            dto.Logo = company.Logo;
            dto.Comment = result.Comment;
            dto.UserName = result.UserName;
            return View(dto);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }

        [HttpGet]
        public IActionResult UserEdit()
        {
            AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            UserEditDto user = appUser.Adapt<UserEditDto>();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditDto userEditDto)
        {
            ModelState.Remove("Password");
            //ModelState.Remove(userEditDto.Password);
            if (ModelState.IsValid)
            {
                AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                appUser.UserName = userEditDto.UserName;
                appUser.FirstName = userEditDto.FirstName;
                appUser.LastName = userEditDto.LastName;
                appUser.PhoneNumber = userEditDto.PhoneNumber;
                appUser.BirthPlace = userEditDto.BirthPlace;
                appUser.BirthDate = userEditDto.BirthDate;
                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(appUser);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(appUser, true);
                    ViewBag.succeeded = "true";
                }
                else
                {
                    AddModelError(result);
                }
            }
            return View(userEditDto);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                if (appUser != null)
                {
                    bool exist = _userManager.CheckPasswordAsync(appUser, changePasswordDto.PasswordOld).Result;
                    if (exist == true)
                    {
                        if (changePasswordDto.PasswordNew == changePasswordDto.PasswordConfirm)
                        {
                            IdentityResult result = _userManager.ChangePasswordAsync(appUser, changePasswordDto.PasswordOld, changePasswordDto.PasswordNew).Result;
                            if (result.Succeeded)
                            {
                                _userManager.UpdateSecurityStampAsync(appUser);
                                _signInManager.SignOutAsync();
                                _signInManager.PasswordSignInAsync(appUser, changePasswordDto.PasswordNew, true, false);
                                ViewBag.status = "Successful";
                            }
                            else
                            {
                                AddModelError(result);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Eski şifreniz yanlıştır");
                    }
                }
            }
            return View(changePasswordDto);
        }

        [HttpGet]
        public IActionResult PassiveUser()
        {
            AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            ViewBag.userName = appUser.UserName;
            var result = (from x in _userManager.Users select new
            {
                IsPassive = x.IsConfirmed
            }).ToList();
            UserPassivizationDto userPassivizationDto = new UserPassivizationDto();
            userPassivizationDto.UserName = appUser.UserName;
            userPassivizationDto.IsConfirmed = appUser.IsConfirmed;

            if (userPassivizationDto.IsConfirmed == true)
            {
                userPassivizationDto.Exist = true;
            }
            else
            {
                userPassivizationDto.Exist = false;
            }
            return View(userPassivizationDto);
        }

        [HttpPost]
        public async Task<IActionResult> PassiveUser(UserPassivizationDto userPassivizationDto)
        {
            AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (userPassivizationDto.Exist)
            {
                appUser.IsConfirmed = true;
            }
            else
            {
                appUser.IsConfirmed = false;
            }
            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(appUser);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(appUser, true);
                ViewBag.succeeded = "true";
                if (appUser.IsConfirmed == false)
                {
                    return RedirectToAction("LogIn", "home");
                }
            }
            else
            {
                AddModelError(result);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Membership()
        {
            MembershipDto membershipDto = new MembershipDto();
            if (User.Identity.Name != null)
            {
                AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                Dictionary<int, string> paketler = new Dictionary<int, string>();
                if (paketler.Count == 0)
                {
                    paketler.Add(1, "6 Aylık");
                    paketler.Add(2, "1 Yıllık");
                    ViewBag.Paket = new SelectList(paketler, "Key", "Value");
                }
            }
            return View(membershipDto);
        }

        [HttpPost]
        public IActionResult Membership(MembershipDto membershipDto)
        {
            AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            UserCompanyDto userCompanyDto = _userCompanyService.GetByUserId(appUser.Id);
            int companyID = (int)userCompanyDto.CompanyId;
            Company company = _companyService.GetById(companyID);
            if (appUser.IsManagerActive == false)
            {
                if (membershipDto.PaketId == 2)
                {
                    company.StartDate = System.DateTime.Now;
                    company.EndDate = System.DateTime.Now.AddYears(1);
                }
                else if (membershipDto.PaketId == 1)
                {
                    company.StartDate = System.DateTime.Now;
                    company.EndDate = System.DateTime.Now.AddMonths(6);
                }
                company.IsConfirmed = true;
                _companyService.Update(company);
            }
            return View();
        }

        public IActionResult CommentDetail()
        {
            return View();
        }
    }
}
