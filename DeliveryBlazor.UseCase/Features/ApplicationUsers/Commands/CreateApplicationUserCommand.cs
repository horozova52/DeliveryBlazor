﻿using DeliveryBlazor.Shared.Enums;
using MediatR;

namespace DeliveryBlazor.UseCase.Features.ApplicationUsers.Commands
{
    public class CreateApplicationUserCommand : IRequest<string>
    {
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public UserRole Role { get; set; }
    }
}
