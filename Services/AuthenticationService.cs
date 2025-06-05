using AutoMapper;
using FluentResults;
using Librarykuno.Errors;
using Librarykuno.Models;
using Librarykuno.Request;
using Librarykuno.Response;
using Librarykuno.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Librarykuno.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMemberRepository _memberRepo;
        private readonly ITokenGeneratorService _tokenService;
        private readonly IMapper _mapper;
        private readonly IPasswordHashingService _passwordHashingService; 

        public AuthenticationService(
            IMemberRepository memberRepo,
            IPasswordHashingService passwordHashingService, 
            ITokenGeneratorService tokenService,
            IMapper mapper)
        {
            _memberRepo = memberRepo;
            _passwordHashingService = passwordHashingService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _memberRepo.ExistsByEmailAsync(request.Email))
            {
                return new AuthenticationResponse()
                {
                    IsSuccess = false,
                    Message = "Email Already Exists"
                };
            }

            var member = new Member
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email
            };

            
            member.PasswordHash = _passwordHashingService.Hash(request.Password, member);

            await _memberRepo.AddAsync(member);

            var token = _tokenService.GenerateToken(member);

            return new AuthenticationResponse()
            {
                IsSuccess = true,
                Message = "Member registered successfully",
                AccessToken = token,
                Member = _mapper.Map<MemberResponse>(member)
            };
        }


        public async Task<AuthenticationResponse> LoginAsync(LoginRequest request)
        {
            var member = await _memberRepo.GetByEmailAsync(request.Email);
            if (member == null)
            {
                return new AuthenticationResponse()
                {
                    IsSuccess = false,
                    Message = "Invalid Email or password."
                };
            }

            if (!_passwordHashingService.Verify(request.Password, member))
            {
                return new AuthenticationResponse()
                {
                    IsSuccess = false,
                    Message = "Invalid Email or password."
                };
            }

            var token = _tokenService.GenerateToken(member);

            return new AuthenticationResponse()
            {
                IsSuccess = true,
                Message = "Member login successfully",
                AccessToken = token,
                Member = _mapper.Map<MemberResponse>(member)
            };
        }

    }
}
