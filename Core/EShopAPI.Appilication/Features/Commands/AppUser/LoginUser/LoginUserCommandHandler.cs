using EShopAPI.Appilication.Abstractions.Token;
using EShopAPI.Appilication.DTOs;
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
        readonly ITokenHandler _tokenHandler;


        public LoginUserCommandHandler(
                UserManager<U.AppUser> userManager,
                SignInManager<U.AppUser> signInManager,
                ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
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
            {
                Token token = _tokenHandler.CreateAccessToken(10);
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }
            //return new LoginUserErrorCommandResponse()
            //{
            //    Messagge = new NotFiniteNumberException().ToString()
            //};

            throw new AuthenticationErrorException();
        }
    }
}