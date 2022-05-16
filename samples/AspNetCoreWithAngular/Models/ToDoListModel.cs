namespace AspNetCoreWithAngular.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ToDoListModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public IEnumerable<ToDoItemModel> ToDoItems { get; set; }
}