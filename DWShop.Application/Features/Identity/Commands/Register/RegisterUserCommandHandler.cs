﻿
using AutoMapper;
using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Application.Interfaces;
using DWShop.Application.Responses.Identity;
using DWShop.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DWShop.Application.Features.Identity.Commands.Register
{
    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, Result<LoginResponse>>
    {
        private readonly IMediator mediator;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;

        public RegisterUserCommandHandler(IMediator mediator,
            UserManager<IdentityUser> userManager, IMapper mapper,
            IAccountService accountService)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.mapper = mapper;
            this.accountService = accountService;
        }
        public async Task<Result<LoginResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<IdentityUser>(request);

            if (await accountService.UserExists(request.UserName))
                return await Result<LoginResponse>
                    .FailAsync("El usuario ya existe");

            user.Id = Guid.NewGuid().ToString();

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return await Result<LoginResponse>
                      .FailAsync(result.Errors
                      .Select(x => x.Description).ToList());

            var loginCommand = new LoginCommand
            { Password = request.Password, UserName = request.UserName };

            return await mediator.Send(loginCommand);

        }
    }
}
