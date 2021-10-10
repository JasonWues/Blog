using SqlSugar;

namespace Blog.Model
{
    public class BlogNews : BaseID
    {
        [SugarColumn(ColumnDataType = "nvarchar(30)")]
        public string? Title { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(30)")]
        public string? Content { get; set; }
        public DateTime Time { get; set; }
        public int BrowseCount { get; set; }
        public int LikeCount { get; set; }
        public int TypeId { get; set; }
        public int WriteId { get; set; }
        [SugarColumn(IsIgnore = true)]
        public TypeInfo? TypeInfo { get; set; }
        [SugarColumn(IsIgnore = true)]
        public WriteInfo? WriteInfo { get; set; }

    }
}
