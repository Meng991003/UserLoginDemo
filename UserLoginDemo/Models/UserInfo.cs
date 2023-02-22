
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserLoginDemo.Models
{
    public class UserLoginInfo
    {
        public string COR_USERID { get; set; }
        public string PASSWORD { get; set; }

        public UserLoginInfo()
        {
            COR_USERID = "";
            PASSWORD = "";
        }
        public UserLoginInfo(string username,string passcode)
        {
            COR_USERID = username;
            PASSWORD = passcode;
        }
    }

    [Table("user_info")]
    public class UserInfo
    {
        public UserInfo()
        {
            COR_USERID = "";
            PASSWORD = "";
        }
        [Key]
        public int ID { get; set; }
        public string COR_USERID { get; set; }

        public string PASSWORD { get; set; }
        public DateTime LOGIN_DTTM { get; set; }

        public DateTime LOGOUT_DTTM { get; set; }

        public int ROLE { get; set; }

        public int PASSWORD_RETRY { get; set; }
        public int MAX_PASSWORD_RETRY { get; set; }
        public Int16 LOGIN_STATUS { get; set; }
        public int ACCESS_RIGHT { get; set; }
        public string USER_NAME { get; set; }
    }
}
