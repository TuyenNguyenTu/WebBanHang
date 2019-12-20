using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name ="Tài khoản")]
        [Required(ErrorMessage ="Bạn phải nhập tài khoản")]
        public string UserName { get; set; }
        [Display(Name ="Mật khẩu")]
        [Required(ErrorMessage ="Bạn phải nhập mật khẩu")]
        public string Password { get; set; }
    }
}