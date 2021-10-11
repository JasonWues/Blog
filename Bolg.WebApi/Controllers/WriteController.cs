using AutoMapper;
using Blog.IService;
using Blog.Model;
using Blog.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility.ApiResult;
using Utility.MD5;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriteController : ControllerBase
    {
        private readonly IWriteInfoService _iWriteInfoService;
        public WriteController(IWriteInfoService iWriteInfoService)
        {
            _iWriteInfoService = iWriteInfoService;
        }
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name, string username, string userpwd)
        {
            //数据效验
            WriteInfo writeInfo = new WriteInfo()
            {
                Name = name,
                UserName = username,
                //加密
                UserPwd = MD5Helper.MD5Encrypt32(userpwd)
            };
            //判断数据库中是否已经存在账号的数据跟要添加的账号相同的数据
            var oldWrite = await _iWriteInfoService.FindAsync(c => c.UserName == username);
            if (oldWrite != null) return ApiResultHelper.Error("账号已存在");
            bool b = await _iWriteInfoService.CreateAsync(writeInfo);
            if (!b) return ApiResultHelper.Error("注册失败");
            return ApiResultHelper.Success(writeInfo);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(User.FindFirst("Id").Value);
            var writeInfo =  await _iWriteInfoService.FindAsync(id);
            writeInfo.Name = name;
            bool b = await _iWriteInfoService.EditAsync(writeInfo);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        [AllowAnonymous]
        [HttpGet("FindWrite")]
        public async Task<ApiResult> Find([FromServices]IMapper iMapper,int id)
        {
            var writeInfo = await _iWriteInfoService.FindAsync(id);
            var writeInfoDTO = iMapper.Map<WriteDTO>(writeInfo);
            return ApiResultHelper.Success(writeInfoDTO);
        }
    }
}
