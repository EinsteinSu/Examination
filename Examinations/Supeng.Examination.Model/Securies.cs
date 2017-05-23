using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supeng.Examination.Model
{
    public class SecurityAction
    {
        [Key]
        public int SecurityActionId { get; set; }

        [MaxLength(20), Required, Display(Name = "行为")]
        public string Name { get; set; }
    }

    public class SecurityRole
    {
        [Key]
        public int SecurityRoleId { get; set; }

        [MaxLength(20), Required, Display(Name = "角色名称")]
        public string Name { get; set; }
    }

    public class SecurityRoleAction
    {
        [Key]
        public int SecurityRoleActionId { get; set; }

        [ForeignKey("Role")]
        public int SecurityRoleId { get; set; }

        public virtual SecurityRole Role { get; set; }

        [ForeignKey("Action")]
        public int SecurityActionId { get; set; }

        public virtual SecurityAction Action { get; set; }
    }
}