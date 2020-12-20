using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Preveld.Models
{
    [Table("webpages_Membership")]
    public class Membership
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserProfile User { get; set; }

        public int IsConfirmed { get; set; }

        public string PasswordVerificationToken { get; set; }

        [DataType(DataType.DateTime)]
        public string PasswordVerificationTokenExpirationDate { get; set; }
    }
}