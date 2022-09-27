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
                Id = Guid.NewGuid().ToString(),   
                UserName = request.username,
                Email = request.email,
                NameSurname = request.nameSurname
            }, request.password);

            CreateUserCommandResponse response = new() { Succeeded=result.Succeeded};

            if (result.Succeeded)
                response.Message = "User created successufully";
            
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }
    }
}