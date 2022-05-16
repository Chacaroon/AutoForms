namespace AspNetCoreWithAngular.Models;

using AutoForms.Attributes;

[FormValue]
public class SelectListItem<T>
{
    public int Id { get; set; }

    public T Value { get; set; }
}