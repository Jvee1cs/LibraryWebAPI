using FluentResults;
using Librarykuno.Request;
using Librarykuno.Response;

namespace Librarykuno.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> RegisterAsync(RegisterRequest request);
        Task<AuthenticationResponse> LoginAsync(LoginRequest request);
    }
}