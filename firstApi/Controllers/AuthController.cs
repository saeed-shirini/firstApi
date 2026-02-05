using firstApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using firstApi.UserModel;
using firstApi.Utils;
using Microsoft.Extensions.Options;

namespace firstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
 

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NoContent))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_authService.CheckIsExistMobile(model.Mobile))
                return BadRequest();

            if (!_authService.Register(model))
                return StatusCode(500, "خطا در ارتباط با سرور");

            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Login(UserModelBinding model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var getUserByMobile = _authService.GetUserByMobile(model.Mobile);
            if (getUserByMobile == null)
                return NotFound();
            var result = _authService.Login(model);
            if (result == null)
            {
                ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است ");
                return StatusCode(500,ModelState);
            }

            return Ok(result);
        }
    }
}
