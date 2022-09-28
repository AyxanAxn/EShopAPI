using T = EShopAPI.Appilication.DTOs;

namespace EShopAPI.Appilication.Abstractions.Token
{
    public interface ITokenHandler
    {
        T.Token CreateAccessToken();
    }
}