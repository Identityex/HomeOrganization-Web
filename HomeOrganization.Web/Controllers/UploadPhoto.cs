using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HomeOrganization.Models.Interfaces;
using HomeOrganization.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeOrganization.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class UploadPhoto : ControllerBase
    {

        private readonly IWebService _webService;

        public UploadPhoto(IWebService webService)
        {
            _webService = webService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImageModel image)
        {
            if (string.IsNullOrEmpty(image.image))
                return BadRequest();

            try
            {
                var userGuid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _webService.UploadImage(userGuid, image.imageName, image.image);
            }
            catch (Exception ex)
            {
                return Problem();
            }
            
            return Ok();
        }
    }
}