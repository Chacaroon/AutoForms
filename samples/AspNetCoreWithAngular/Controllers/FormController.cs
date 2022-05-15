namespace AspNetCoreWithAngular.Controllers
{
    using AspNetCoreWithAngular.Models;
    using AutoForms;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly FormBuilderFactory _formBuilderFactory;

        private static SchoolModel _schoolModel;

        public FormController(FormBuilderFactory formBuilderFactory)
        {
            _formBuilderFactory = formBuilderFactory;
        }

        [HttpGet("create")]
        public IActionResult Get()
        {
            var form = _formBuilderFactory.CreateFormBuilder<SchoolModel>()
                .EnhanceWithValidators()
                .Build();

            return Ok(form);
        }

        [HttpPost("create")]
        public IActionResult Post([FromBody] SchoolModel model)
        {
            _schoolModel = model;

            return Ok(model);
        }

        [HttpGet("update")]
        public IActionResult GetFormWithValue()
        {
            var form = _formBuilderFactory.CreateFormBuilder<SchoolModel>()
                .EnhanceWithValidators()
                .EnhanceWithValue(_schoolModel)
                .Build();

            return Ok(form);
        }
    }
}
