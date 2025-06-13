using AutoMapper;
using LibraryManagement.Application.Dtos.Request;
using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Domain.BookContext.Entities;
using LibraryManagement.Domain.MemberContext.Entities;
using LibraryManagement.Infrastructure.Data.Entities;

namespace LibraryManagement.Infrastructure.Mapping
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

            CreateMap<BookResponse, Book>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN));
            
            CreateMap<BookEntity, BookResponse>();
            CreateMap<MemberEntity, BorrowerResponse>();
            CreateMap<MemberEntity, MemberResponse>();
            CreateMap<UpdateMemberRequest, Member>();
            CreateMap<UpdateBookRequest, Book>();
            CreateMap<Book, BookResponse>();
            CreateMap<Member, MemberResponse>();

            CreateMap<Book, BorrowedBookResponse>();
            CreateMap<Member, BorrowerResponse>();
            

        }
    }
}