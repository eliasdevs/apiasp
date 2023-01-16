using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universityApiBackend.Models.DataModels
{
    public class Category:BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; }=string.Empty;
        [Required, StringLength(50)]
        public ICollection<Course> Courses { get; set; }=new List<Course>();



    }
}
