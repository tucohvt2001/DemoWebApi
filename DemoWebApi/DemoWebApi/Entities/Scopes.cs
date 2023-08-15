using System.ComponentModel.DataAnnotations;

namespace DemoWebApi.Entities
{
    public class Scopes
    {
        [Key] public int ScopeId { get; set; }
        public string ScopeName { get; set; }
    }
}
