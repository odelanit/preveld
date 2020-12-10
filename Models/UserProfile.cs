using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Preveld.Models
{
    [Table("user_profile_db")]
    public class UserProfile
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Full name")]
        public string Full_Name { get; set; }

        [DisplayName("Username")]
        [Required]
        [Remote("IsUsernameAvailable", "Profile", ErrorMessage = "Username Already Exist.")]
        public string User_ID { get; set; }

        [DisplayName("Password")]
        [Required]
        public string User_PW { get; set; }

        [DisplayName("User Contact(Email)")]
        public string Email { get; set; }

        [DisplayName("User Contact(Phone)")]
        public string Phone { get; set; }
    }
}