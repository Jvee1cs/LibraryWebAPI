using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryManagement.Domain.MemberContext.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Application.Services
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(Member member);
    }
}
