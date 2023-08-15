using System.ComponentModel.DataAnnotations;

namespace DemoWebApi.Entities
{
    public class UserTags
    {
        [Key] public int UserTagId { get; set; }
        public int TagId { get; set; }
        public long UserId { get; set; }

    }
}
