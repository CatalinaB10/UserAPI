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
        [Column("Id")]
        public Guid Id { get; set; }

        [Required] // NOT NULL in the database
        [MaxLength(100)] // VARCHAR(100)
        [Column("Name")]
        public string Name { get; set; }

        [Required] // NOT NULL in the database
        [MaxLength(100)] // VARCHAR(100)
        [Column("Role")]
        public string Role { get; set; }

        [Column("Devices")]
        public ICollection<Device>? Devices { get; set; } = new List<Device>();
    }
}
