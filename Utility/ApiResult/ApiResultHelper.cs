using SqlSugar;

namespace Utility.ApiResult
{
    public class ApiResultHelper
    {
        public static ApiResult Success(object data)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg = "成功",
                Total = 0
            };
        }
        public static ApiResult Success(object data, RefAsync<int> total)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg = "成功",
                Total = total
            };
        }
        public static ApiResult Error(string msg)
        {
            return new ApiResult
            {
                Code = 500,
                Data = null,
                Msg = msg,
                Total = 0
            };
        }
    }
}
