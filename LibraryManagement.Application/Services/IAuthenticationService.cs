using LibraryManagement.Application.Dtos.Request;
using LibraryManagement.Application.Dtos.Response;

namespace LibraryManagement.Application.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> RegisterAsync(RegisterRequest request);
        Task<AuthenticationResponse> LoginAsync(LoginRequest request);
    }
}