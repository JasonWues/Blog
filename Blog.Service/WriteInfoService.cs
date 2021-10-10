using Blog.IRepository;
using Blog.IService;
using Blog.Model;

namespace Blog.Service
{
    public class WriteInfoService : BaseService<WriteInfo>, IWriteInfoService
    {
        private readonly IWriteInfoRepository _iWriteInfoRepository;
        public WriteInfoService(IWriteInfoRepository iWriteInfoRepository)
        {
            base._iBaseRepository = iWriteInfoRepository;
        }
    }
}