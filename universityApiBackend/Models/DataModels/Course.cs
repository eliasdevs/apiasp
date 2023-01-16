using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace universityApiBackend.Models.DataModels
{
    public enum Nivel
    {
        Basic, Intermediate ,Advanced
    }

    public class Course: BaseEntity
    {
       
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280, MinimumLength = 10, ErrorMessage = "No menos de 10 y no mas 280 Caracteres")]
        public string ShortDescription{ get; set; } = string.Empty;

        
        [Required]        
        public string LargeDescription { get; set; } = string.Empty;

        [Required]
        public string PublicoObjetivo { get; set; } = string.Empty;

        [Required]
        public string Objetivos{ get; set; } = string.Empty;
        
        [Required]
        public string Requisitos { get; set; } = string.Empty;


        [DisplayFormat(NullDisplayText = "No grade")]
        public Nivel Nivel { get; set; }=Nivel.Basic;
        public ICollection<Category> Categories { get; set; }=new List<Category>();

        public Chapter Chapter { get; set; } = new Chapter();
        public ICollection<Student> Students { get; set; }=new List<Student>();
    }

}
