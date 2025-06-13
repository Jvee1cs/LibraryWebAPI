using System.Security.Cryptography;
using System.Text;
using LibraryManagement.Domain.MemberContext.Entities;
using LibraryManagement.Application.Services;

namespace LibraryManagement.Infrastructure.Services
{
   
    public class PasswordHashingService : IPasswordHashingService
    {
        public string Hash(string password, Member member)
        {
            
            var combined = password + member.Email.ToLower(); 
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(combined);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool Verify(string providedPassword, Member member)
        {
            var hashedInput = Hash(providedPassword, member);
            return hashedInput == member.PasswordHash;
        }
    }
}