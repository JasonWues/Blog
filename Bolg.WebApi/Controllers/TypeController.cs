using Blog.IService;
using Blog.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility.ApiResult;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeInfoService _iTypeInfoService;
        public TypeController(ITypeInfoService itTypeInfoService)
        {
            _iTypeInfoService = itTypeInfoService;
        }

        [HttpGet("Types")]
        public async Task<ApiResult> Types()
        {
            var type = await _iTypeInfoService.QueryAsync();
            if (type.Count == 0) return ApiResultHelper.Error("没有更多的类型");
            return ApiResultHelper.Success(type);
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("类型名不能为空");
            TypeInfo type = new TypeInfo()
            {
                Name = name
            };
            bool b = await _iTypeInfoService.CreateAsync(type);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(b);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int id, string name)
        {
            var type = await _iTypeInfoService.FindAsync(id);
            if (type == null) return ApiResultHelper.Error("没有找到文章类型");
            type.Name = name;
            bool b = await _iTypeInfoService.EditAsync(type);
            if (!b) return ApiResultHelper.Success("修改失败");
            return ApiResultHelper.Success(type);
        }

        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int id)
        {
            bool b = await _iTypeInfoService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }
    }
}
