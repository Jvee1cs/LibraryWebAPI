using AutoMapper;
using Librarykuno.Models;
using Librarykuno.Request;
using Librarykuno.Response;

namespace Librarykuno.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MemberRequest, Member>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.BorrowedBooksCount, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.MaxBooksAllowed, opt => opt.MapFrom(_ => 3));

            CreateMap<BookRequest, Book>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.IsBorrowed, opt => opt.MapFrom(_ => false));

            CreateMap<Book, BookResponse>();
            CreateMap<Member, MemberResponse>();

            CreateMap<Book, BorrowedBookResponse>();
            CreateMap<Member, BorrowerResponse>();
        }
    }
}