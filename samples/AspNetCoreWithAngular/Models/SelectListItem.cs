namespace AspNetCoreWithAngular.Models;

public class SelectListItem<T>
{
    public int Id { get; set; }

    public T Value { get; set; }
}