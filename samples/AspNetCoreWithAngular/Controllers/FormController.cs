namespace AspNetCoreWithAngular.Controllers
{
    using AspNetCoreWithAngular.Models;
    using AutoForms;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly FormResolver _formResolver;

        private static SchoolModel _schoolModel;

        public FormController(FormResolver formResolver)
        {
            _formResolver = formResolver;
        }

        [HttpGet("create")]
        public IActionResult Get()
        {
            var form = _formResolver.Resolve<SchoolModel>();

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
            var form = _formResolver.Resolve(_schoolModel);

            return Ok(form);
        }
    }
}
