using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeviceAPI.Models;

namespace UserAPI.Models
{
    [Table("Users", Schema = "public")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // BIGSERIAL maps to identity in C#
        public long Id { get; set; }

        [Required] // NOT NULL in the database
        [MaxLength(100)] // VARCHAR(100)
        public string Name { get; set; }

        [Required] // NOT NULL in the database
        [MaxLength(100)] // VARCHAR(100)
        public string Role { get; set; }

        public IEnumerable<Device> Devices;
    }
}
