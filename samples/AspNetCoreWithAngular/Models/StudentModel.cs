namespace AspNetCoreWithAngular.Models
{
    using System.ComponentModel.DataAnnotations;

    public class StudentModel
    {
        [Required]
        public string Name { get; set; }
    }
}
