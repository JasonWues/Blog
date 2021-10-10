using SqlSugar;

namespace Blog.Model
{
    public class TypeInfo : BaseID
    {
        [SugarColumn(ColumnDataType = "nvarchar(12)")]
        public string? Name { get; set; }
    }
}
