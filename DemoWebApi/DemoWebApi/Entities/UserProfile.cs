using System.ComponentModel.DataAnnotations;

namespace DemoWebApi.Entities
{
    public class UserProfile
    {
        [Key] public long UserId { get; set; }

        public int UserGender { get; set; }

        public long UserAppId { get; set; }

        public bool IsSensitive { get; set; }

        public string DisplayName { get; set; }

        public string BirthDate { get; set; }
    }
}
