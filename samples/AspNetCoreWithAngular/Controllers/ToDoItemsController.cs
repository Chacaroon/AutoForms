using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreWithAngular.Models;
using AutoForms;

namespace AspNetCoreWithAngular.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private readonly FormBuilderFactory _formBuilderFactory;

    private static readonly Dictionary<int, ToDoListModel> _toDoItems = new()
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
        return Ok(_toDoItems.Select(x => x.Value));
    }


    [HttpGet("create")]
    public IActionResult Create()
    {
        var form = _formBuilderFactory.CreateFormBuilder<ToDoListModel>()
            .Build();

        return Ok(form);
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] ToDoListModel model)
    {
        var id = _toDoItems.Keys.LastOrDefault() + 1;
        model.Id = id;
        _toDoItems.Add(id, model);

        return Ok();
    }

    [HttpGet("update/{id}")]
    public IActionResult Update(int id)
    {
        var form = _formBuilderFactory.CreateFormBuilder<ToDoListModel>()
            .EnhanceWithValue(_toDoItems[id])
            .Build();

        return Ok(form);
    }

    [HttpPost("update/{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] ToDoListModel model)
    {
        _toDoItems[id] = model;

        return Ok();
    }
}