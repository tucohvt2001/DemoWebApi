using System.ComponentModel.DataAnnotations;

namespace DemoWebApi.Entities
{
    public class ScopeTags
    {
        [Key] public int ScopeTagId { get; set; }
        public int ScopeId { get; set; }
        public int TagId { get; set; }
    }
}
