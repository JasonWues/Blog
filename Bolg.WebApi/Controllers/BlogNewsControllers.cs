using Blog.IService;
using Blog.Model;
using Blog.WebApi.Utility.ApiResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsControllers : ControllerBase
    {
        private readonly IBlogNewsService _iBlogNewsService;
        public BlogNewsControllers(IBlogNewsService iBlogNewsService)
        {
            this._iBlogNewsService = iBlogNewsService;
        }
        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
           var data =  await _iBlogNewsService.QueryAsync();
           if (data==null) return ApiResultHelper.Error("没有更多的的文章");
           return ApiResultHelper.Success(data);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> Create(string title,string content,int typeid)
        {
            BlogNews blogNews = new BlogNews()
            {
                BrowseCount = 0,
                LikeCount = 0,
                Content = content,
                Time = DateTime.Now,
                TypeId = typeid,
                Title = title,
                WriteId = 1,
            };
            bool b = await _iBlogNewsService.CreateAsync(blogNews);
            if (!b) return ApiResultHelper.Error("添加失败，服务器发生错误");
            return ApiResultHelper.Success(blogNews);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _iBlogNewsService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id, string title, string content, int typeid)
        {
            var blogNews = await _iBlogNewsService.FindAsync(id);
            if (blogNews == null) return ApiResultHelper.Error("没有找到该文章"); 
            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeid;
            bool b = await _iBlogNewsService.EditAsync(blogNews);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(blogNews);
        }
    }
}
