using System.ComponentModel.DataAnnotations;

namespace MemberAuth.Models
{
    public class UserLogins
    {
        [Required]
        public string Userid
        {
            get;
            set;
        }
        [Required]
        public string Password
        {
            get;
            set;
        }
        public UserLogins() { }
    }
}
