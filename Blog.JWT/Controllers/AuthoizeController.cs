using Blog.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility.ApiResult;

namespace Blog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly IWriteInfoService _iWriteInfoService;
        public AuthoizeController(IWriteInfoService iWriteInfoService)
        {
            _iWriteInfoService = iWriteInfoService;
        }
        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username, string userpwd)
        {
            //数据校验
            
        }
    }
}
