﻿using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Application.Responses.Identity;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Identity.Login
{
    public interface IAuthenticationManager : IManager
    {
        Task<IResult<LoginResponse>> Login(LoginCommand command);
    }
}