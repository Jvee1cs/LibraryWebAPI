using System.Security.Claims;
using AutoMapper.Execution;
using Librarykuno.Errors;
using Librarykuno.Request;
using Librarykuno.Response;
using Librarykuno.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Member = Librarykuno.Models.Member;

namespace Librarykuno.Controllers
{
    // Controllers/MembersController.cs
    [ApiController]
    [Authorize]
    [Route("api/authentication/members")]
    public class MembersController : ControllerBase
    {
        private readonly ILibraryService _library;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="library"></param>
        public MembersController(ILibraryService library) => _library = library;


        [HttpGet("me")]
        public async Task<IActionResult> GetMember()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();
            var member = await _library.GetMemberById(Guid.Parse(userId));
            return member is null ? NotFound() : Ok(member);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMember()
        {
            var member = await _library.GetAllMember();
            return Ok(member);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMember(Guid memberId)
        {
         
            var result = await _library.DeleteMember(memberId);

            if (result.IsFailed && result.HasError<MemberNotFoundError>())
                return NotFound(new { message = result.Errors.First().Message });

            return Ok(new { message = result.Successes.FirstOrDefault()?.Message });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember( [FromBody] UpdateMemberRequest dto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();
            var result = await _library.UpdateMemberAsync(Guid.Parse(userId), dto);

            if (result.IsFailed)
                return NotFound(result.Errors.First().Message);

            return Ok(result.Successes.First().Message);
        }


    }


}
