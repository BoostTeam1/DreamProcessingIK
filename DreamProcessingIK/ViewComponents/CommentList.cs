using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DreamProcessingIK.ViewComponents
{
    public class CommentList:ViewComponent
    {
        public UserManager<AppUser> _userManager;
        public CommentList(UserManager<AppUser> user)
        {
            _userManager = user;
        }
        public IViewComponentResult Invoke()
        {

            List<AppUser> commentList = new List<AppUser>(); 
            var result = _userManager.Users.ToList();
            foreach (var item in result)
            {
                if (item.Comment!=null)
                {
                    commentList.Add(item);
                }
            }
            return View(commentList);
        }
    }
}
