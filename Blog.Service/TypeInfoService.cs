using Blog.IRepository;
using Blog.IService;
using Blog.Model;

namespace Blog.Service
{
    public class TypeInfoService : BaseService<TypeInfo>, ITypeInfoService
    {
        private readonly ITypeInfoRepository _iBlogNewsRepository;
        public TypeInfoService(ITypeInfoRepository iTypeInfoRepository)
        {
            base._iBaseRepository = iTypeInfoRepository;
        }
    }
}