using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UserAPI.Models
{
    [Table("Users", Schema = "public")]
    public class User
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = new Guid();  

        [Required] // NOT NULL in the database
        [MaxLength(100)] // VARCHAR(100)
        [Column("Name")]
        public string Name { get; set; }

        [Required] // NOT NULL in the database
        [MaxLength(100)] // VARCHAR(100)
        [Column("Role")]
        public string Role { get; set; }

        //[Column("Devices")]
        //public IEnumerable<DeviceDTO>? Devices { get; set; } = new List<DeviceDTO>();
    }
}
