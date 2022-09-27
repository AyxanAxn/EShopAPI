using Microsoft.AspNetCore.Identity;

namespace EShopAPI.Domain.Entities.Identity
{
    public class AppUser:IdentityUser<string>
    {
        public string NameSurname { get; set; }
    }
}