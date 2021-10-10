using Blog.IRepository;
using Blog.IService;
using Blog.Model;

namespace Blog.Service
{
    public class BlogNewsService : BaseService<BlogNews>, IBlogNewsService
    {
        private readonly IBlogNewsRepository _iBlogNewsRepository;
        public BlogNewsService(IBlogNewsRepository iBlogNewsRepository)
        {
            base._iBaseRepository = iBlogNewsRepository;
        }
    }
}