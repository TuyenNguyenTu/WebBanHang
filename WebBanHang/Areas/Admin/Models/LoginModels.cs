using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Areas.Admin.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Mời nhập tài khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}