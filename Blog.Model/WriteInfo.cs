using SqlSugar;

namespace Blog.Model
{
    public class WriteInfo : BaseID
    {
        [SugarColumn(ColumnDataType = "nvarchar(20)")]
        public string? Name { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(16)")]
        public string? UserName { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(64)")]
        public string? UserPwd { get; set; }
    }
}
