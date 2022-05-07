namespace AspNetCoreWithAngular.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SchoolModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        public ClassModel[] Students { get; set; }

        public SchoolOptions Options { get; set; }
    }
}
