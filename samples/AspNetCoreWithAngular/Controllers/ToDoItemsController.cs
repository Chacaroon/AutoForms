using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWithAngular.Controllers;

using System.Collections.Generic;
using System.Linq;
using AspNetCoreWithAngular.Models;
using AutoForms;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private readonly FormBuilderFactory _formBuilderFactory;

    private static readonly Dictionary<int, ToDoListModel> ToDoItems = Enumerable.Range(1, 3).ToDictionary(x => x, x => new ToDoListModel
    {
        Id = x,
        Name = $"List {x}",
        ToDoItems = Enumerable.Range(1, 3).Select(x =>
            new ToDoItemModel
            {
                Name = $"Item {x}",
                Done = x % 2 == 0
            })
    });

    public ToDoItemsController(FormBuilderFactory formBuilderFactory)
    {
        _formBuilderFactory = formBuilderFactory;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(ToDoItems.Select(x => x.Value));
    }


    [HttpGet("create")]
    public IActionResult Create()
    {
        var form = _formBuilderFactory.CreateFormBuilder<ToDoListModel>()
            .EnhanceWithValidators()
            .Build();

        return Ok(form);
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] ToDoListModel model)
    {
        var id = ToDoItems.Keys.LastOrDefault() + 1;
        model.Id = id;
        ToDoItems.Add(id, model);

        return Ok();
    }

    [HttpGet("update/{id}")]
    public IActionResult Update(int id)
    {
        var form = _formBuilderFactory.CreateFormBuilder<ToDoListModel>()
            .EnhanceWithValidators()
            .EnhanceWithValue(ToDoItems[id])
            .Build();

        return Ok(form);
    }

    [HttpPost("update/{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] ToDoListModel model)
    {
        ToDoItems[id] = model;

        return Ok();
    }
}