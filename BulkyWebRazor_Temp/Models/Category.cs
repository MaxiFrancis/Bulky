using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRazor_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Dysplay Order")]
        [Range(1, 100, ErrorMessage = "The field Dysplay Order must be between 1 and 100.\r\n")]
        public int DisplayOrder { get; set; }

    }
}
