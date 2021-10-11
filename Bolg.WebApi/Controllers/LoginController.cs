using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Bcpg;
using Utility.ApiResult;
using Utility.MD5;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IWriteInfoService _iWriteInfoService;
        public LoginController(IWriteInfoService iWriteInfoService)
        {
            _iWriteInfoService = iWriteInfoService;
        }

        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username, string userpwd)
        {
            string pwd = MD5Helper.MD5Encrypt32(userpwd);
            //数据校验
            var writeInfo = await _iWriteInfoService.FindAsync(x => x.UserName == username && x.UserPwd == pwd);
            if (writeInfo != null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, writeInfo.Name),
                    new Claim("Id", writeInfo.Id.ToString()),
                    new Claim("UserName", writeInfo.UserName)
                    //不能放敏感信息
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXXC-PRZ5-SAD-DFSFA-METATRX-ON"));
                //issuer代表颁发Token的Web应用程序,audience是Token的受理者
                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7155",
                    audience: "https://localhost:7155",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHelper.Success(jwtToken);
            }
            else
            {
                return ApiResultHelper.Error("账号密码错误");
            }
        }
    }
}
