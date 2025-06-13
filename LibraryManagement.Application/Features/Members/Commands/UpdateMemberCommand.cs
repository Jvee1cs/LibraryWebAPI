using FluentResults;
using LibraryManagement.Application.Dtos.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Features.Members.Commands
{
    public record UpdateMemberCommand(Guid MemberId, UpdateMemberRequest Dto) : IRequest<Result>;


}
