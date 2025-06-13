using AutoMapper;
using FluentResults;
using LibraryManagement.Application.Errors;
using LibraryManagement.Application.Features.Members.Commands;
using LibraryManagement.Domain.MemberContext.Repositories;
using MediatR;

public class UpdateMemberHandler : IRequestHandler<UpdateMemberCommand, Result>
{
    private readonly IMemberRepository _memberRepo;
    private readonly IMapper _mapper;

    public UpdateMemberHandler(IMemberRepository memberRepo, IMapper mapper)
    {
        _memberRepo = memberRepo;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateMemberCommand request, CancellationToken ct)
    {
        var member = await _memberRepo.GetByIdAsync(request.MemberId);
        if (member is null)
            return Result.Fail(new EntityNotFoundError(request.MemberId));

        _mapper.Map(request.Dto, member);

        await _memberRepo.UpdateAsync(member);
        return Result.Ok().WithSuccess("Member updated successfully");
    }
}
