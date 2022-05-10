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

        public FormController(FormResolver formResolver)
        {
            _formResolver = formResolver;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var form = _formResolver.Resolve<SchoolModel>();

            return Ok(form);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SchoolModel model)
        {
            return Ok(model);
        }
    }
}
