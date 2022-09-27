using EShopAPI.Appilication.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using U = EShopAPI.Domain.Entities.Identity;

namespace EShopAPI.Appilication.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<U.AppUser> _userManager;
        readonly SignInManager<U.AppUser> _signInManager;

        public LoginUserCommandHandler(
                UserManager<U.AppUser> userManager,
                SignInManager<U.AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            U.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user is null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user is null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            { }
            return new();
        }
    }
}
