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

    private static readonly Dictionary<int, ToDoListModel> ToDoItems = new()
    {
        {
            1,
            new ToDoListModel
            {
                Id = 1,
                Name = "Іванов В. О.",
                Tags = new[] { 1, 2 },
                ToDoItems = new[]
                {
                    new ToDoItemModel
                    {
                        Name = "Математика",
                        Done = false
                    },
                    new ToDoItemModel
                    {
                        Name = "Фізика",
                        Done = true
                    },
                    new ToDoItemModel
                    {
                        Name = "Програмування",
                        Done = false
                    },
                }
            }
        },
        {
            2,
            new ToDoListModel
            {
                Id = 1,
                Name = "Петров Б. Б.",
                Tags = new[] { 2, 3 },
                ToDoItems = new[]
                {
                    new ToDoItemModel
                    {
                        Name = "Математика",
                        Done = true
                    },
                    new ToDoItemModel
                    {
                        Name = "Фізика",
                        Done = true
                    },
                    new ToDoItemModel
                    {
                        Name = "Програмування",
                        Done = false
                    },
                }
            }
        }
    };

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