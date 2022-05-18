namespace AspNetCoreWithAngular.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoForms.Attributes;

public class ToDoListModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [FormValue]
    public IEnumerable<int> Tags { get; set; }

    public IEnumerable<ToDoItemModel> ToDoItems { get; set; }
}