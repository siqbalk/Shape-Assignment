﻿using DevAssignment.CommonLayer.Core.ResponseModels;
using DevAssignment.CommonLayer.Dtos;

namespace DevAssignment.ServiceLayer.Services
{
    public interface IUserService
    {
        Task<Response> RegisterUser(RegisterUserDto registerUser);
    }
}