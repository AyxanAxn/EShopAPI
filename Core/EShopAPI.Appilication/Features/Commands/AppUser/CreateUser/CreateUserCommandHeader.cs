using EShopAPI.Appilication.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using U = EShopAPI.Domain.Entities.Identity;
namespace EShopAPI.Appilication.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHeader : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<U.AppUser> _userManager;

        public CreateUserCommandHeader(UserManager<U.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                UserName = request.username,
                Email = request.email,
                NameSurname = request.nameSurname
            }, request.password);

            if (result.Succeeded)
            {
                return new()
                {
                    Succeeded = true,
                    Message = "User created successufully"
                };
            }
            

            throw new UserCreateFailedException();
        }
    }
}