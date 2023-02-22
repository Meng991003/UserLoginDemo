
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserLoginDemo.Models
{
    [Table("user_log")]
    public class UserLog
    {
        [Key]
        public int SEQ { get; set; }
        public string COR_USERID { get; set; }
        public string ACTION { get; set; }

        public DateTime ACCESS_TIME { get; set; }

    }
}
