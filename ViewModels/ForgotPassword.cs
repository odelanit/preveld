using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Preveld.ViewModels
{
    public class ForgotPassword
    {
        [Required]
        public String Email { get; set; }
    }
}