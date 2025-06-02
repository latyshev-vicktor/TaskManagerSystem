using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.UseCases.User.Dto
{
    public record TokenResponse(string AccessToken, string RefreshToken);
}
