using System.ComponentModel.DataAnnotations;

namespace DemoWebApi.Entities
{
    public class Tags
    {
        [Key] public int TagId { get; set; }
        public string TagName { get; set; }
    }
}
