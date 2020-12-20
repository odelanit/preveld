using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Preveld.ViewModels
{
    public class ResetPassword
    {
        [Required]
        public String Email { get; set; }

        public String Password { get; set; }

        public String PasswordConfirm { get; set; }

        public String ReturnToken { get; set; }
    }
}