using LibraryManagement.Domain.MemberContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public interface IPasswordHashingService
    {
        string Hash(string password, Member member);
        bool Verify(string providedPassword, Member member);
    }
}
