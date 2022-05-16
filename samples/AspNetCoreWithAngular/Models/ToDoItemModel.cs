namespace AspNetCoreWithAngular.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ToDoItemModel
    {
        [Required]
        public string Name { get; set; }

        public bool Done { get; set; }
    }
}
