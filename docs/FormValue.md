# FormValue attribute

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

The frontend part obtains and process the form model as usual. The only thing is that the `ng-select` component links to the `tags` control with the `formControlName` attribute and not the `formArrayName`.

```html
<div class="container" *ngIf="form$ | async as form" [formGroup]="form">
    ...
    <label class="w-50"> Tags:
        <div class="mb-3">
            <ng-select
                formControlName="tags"
                [items]="tags"
                bindLabel="value"
                bindValue="id"
                [multiple]="true">
            </ng-select>
            <span *ngFor="let pair of form.controls.tags.errors | keyvalue" style="color: red">
                {{pair.value}}
            </span>
        </div>
    </label>
</div>
```
