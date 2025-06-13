using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Features.Members.Commands
{
    public record DeleteMemberCommand(Guid Member) : IRequest<Result>;

}
