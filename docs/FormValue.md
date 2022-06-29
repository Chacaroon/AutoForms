# FormValue attribute

## Overview

Sometimes it's necessary to generate the `FormControl` based on the complex type such as collection, built in or complex class or sctuct. By default, AutoForms translates collections to `FormArray`s and complex types to `FormGroup`s. If you need generate `FormControl` instead, you can use `FormValue` attribute.

## FormValue attribute usage

Let's see at the `ToDoListModel` class from [AspNetCoreWithAngular](https://github.com/Chacaroon/AutoForms/tree/master/samples/AspNetCoreWithAngular) sample. The `Tags` property has `[FormValue]` attribure whitch means this poprerty will be translated to the `FormControl` that holds a collection-like value.

```csharp
public class ToDoListModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [FormValue]
    [MinLength(2)]
    public IEnumerable<int> Tags { get; set; }

    public IEnumerable<ToDoItemModel> ToDoItems { get; set; }
}
```
