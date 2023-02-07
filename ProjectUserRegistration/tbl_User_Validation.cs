using System.ComponentModel.DataAnnotations;

namespace ProjectUserRegistration
{
    public class tbl_User_Validation
    {
        [Required(ErrorMessage = "Please Enter User Name")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Select Your Gender")]
        public string Gender { get; set; }
    }
    [MetadataType(typeof(tbl_User_Validation))]
    public partial class tbl_User
    { }
}