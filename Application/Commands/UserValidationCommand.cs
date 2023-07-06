using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UserValidationCommand :IRequest<string>
    {
        public string? UserId { get; set; }

        public UserValidationCommand(string userId)
        {
            UserId = userId;
        }


    }
}
