using SqlSugar;

namespace Blog.Model
{
    public class BaseID
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }
    }
}
