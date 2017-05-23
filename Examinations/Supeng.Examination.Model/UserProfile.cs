using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Supeng.Examination.Model
{
    public class UserProfile
    {

        [Key]
        public int UserId { get; set; }


        [Display(Name = "用户代码"), Required, MaxLength(6)]
        public string UserCode { get; set; }

        [MaxLength(20), Required]
        [Display(Name = "用户名")]
        public string Name { get; set; }


        [ForeignKey("Site")]
        [Display(Name = "网点ID")]
        public int? SiteId { get; set; }

        public virtual Site Site { get; set; }

        [ForeignKey("SecurityRole")]
        [Display(Name = "角色ID")]
        public int? SecurityRoleId { get; set; }

        public virtual SecurityRole SecurityRole { get; set; }

        [NotMapped]
        public string[] Roles { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "性别")]
        public Gender Gender { get; set; }

        [MaxLength(11)]
        [Display(Name = "手机")]
        public string Mobile { get; set; }

        [Display(Name = "备注")]
        public string Description { get; set; }

        public void EncryptPassword()
        {
            Password = Encrypt(Password);
        }

        public bool ComparePassword(string password)
        {
            return Password == Encrypt(password);
        }

        protected virtual string Encrypt(string input)
        {
            MD5 md5 = new MD5Cng();
            byte[] buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (var b in buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }

    public enum Gender
    {
        男, 女
    }
}