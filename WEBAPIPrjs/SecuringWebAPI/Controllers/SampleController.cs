using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuringWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecuringWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ITokenRepository _tokenRepository;
        //private readonly ILogger _logger;
        public SampleController(ITokenRepository tokenRepository/*, ILogger logger*/)
        {
            _tokenRepository = tokenRepository;
            //_logger = logger;
        }

        // GET: api/<SampleController>
        [HttpGet]
        public IActionResult Get()
        {
            //try
            //{
             ApiResponse<string[]> response = new ApiResponse<string[]>();

            int a = 10;
            int b = 5;
            int result = a / b;

            //return Ok(new string[] { "value1", "value2" });

            response.Data = new string[] { "value1", "value2" };
            return Ok(response);
            //}
            //catch (DivideByZeroException ex)
            //{
            //    ApiResponse<string> response = new ApiResponse<string>();
            //    response.Success = false;
            //    response.ErrorMessage= ex.Message;
            //    return Ok(response);
            //}
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users userData)
        {
            //try
            //{
            ApiResponse<Tokens> responseSuccess = new ApiResponse<Tokens>();

            var token = _tokenRepository.Authenticate(userData);
            if (token == null)
            {
                ApiResponse<string> responseUnAuthorized = new ApiResponse<string>();
                responseUnAuthorized.Success = false;
                responseUnAuthorized.ErrorMessage = "You are not authorized, check your credentials";
                return Ok(responseUnAuthorized);
                //return Unauthorized();
            }
            else
            {
                responseSuccess.Data = token;
                return Ok(responseSuccess);
                //return Ok(token);
            }
            //}
            //catch(Exception ex)
            //{
            //    //Log the exception error 
            //    //_logger.LogError(ex.Message, ex);

            //    ApiResponse<string> apiExcepResponse = new ApiResponse<string>();
            //    apiExcepResponse.Success = false;
            //    apiExcepResponse.ErrorMessage = "some internal server occurred, contact your admin";
            //    return Ok(apiExcepResponse);
            //}
        }

    }
}
