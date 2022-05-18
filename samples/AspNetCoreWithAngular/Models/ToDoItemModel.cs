namespace AspNetCoreWithAngular.Models;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ToDoItemModel
{
    [Required]
    public string Name { get; set; }

    public bool Done { get; set; }
}