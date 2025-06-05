using FluentResults;
using Librarykuno.Errors;
using Librarykuno.Models;
using Librarykuno.Request;
using Librarykuno.Response;
using Librarykuno.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Librarykuno.Controllers
{
    // Controllers/BooksController.cs
    [ApiController]
    [Authorize]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryService _library;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="library"></param>
        public BooksController(ILibraryService library) => _library = library;


        /// <summary>
        /// Create book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <response code="201">When creating books created</response>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookRequest book)
        {
            var result = await _library.AddNewBook(book);
            return CreatedAtRoute(nameof(GetBook),  new{result.Id},result);
        }

        
        [HttpGet("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _library.GetBookById(id);
            return book is null ? NotFound() : Ok(book);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAvailableBooks()
        {
            var books = await _library.SearchAvailableBooks();
            return Ok(books);
        }

        [HttpGet("overdue")]
       
        public async Task<IActionResult> GetOverdueBooks()
        {
            var books = await _library.GetOverdueBooks();
            return Ok(books);
        }

        [HttpPost("{bookId}/borrow")]
        public async Task<IActionResult> BorrowBook(Guid bookId )
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();
            var result = await _library.BorrowBook(Guid.Parse(userId), bookId);

            if (result.HasError<EntityNotFoundError>(out var notFoundErrors))
                return NotFound(new { message = notFoundErrors.FirstOrDefault()?.Message });

            if (result.HasError<BookAlreadyBorrowedError>(out var borrowErrors))
                return Conflict(new { message = borrowErrors.FirstOrDefault()?.Message });

            if (result.HasError<BorrowingLimitReachedError>(out var limitErrors))
                return StatusCode(429, new { message = limitErrors.FirstOrDefault()?.Message });

            if (result.IsFailed)
                return StatusCode(500, new { message = "Unknown error occurred." });

            return CreatedAtRoute(nameof(GetBook), new { id = bookId }, new { message = result.Successes.FirstOrDefault()?.Message });
        }

        [HttpPost("{bookId}/return")]
        public async Task<IActionResult> ReturnBook(Guid bookId )
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();
            var result = await _library.ReturnBook(Guid.Parse(userId), bookId);
          
            if (result.HasError<EntityNotFoundError>(out var errors))
                return BadRequest(errors.FirstOrDefault()?.Message);

            return Ok(new { message = result.Successes.FirstOrDefault()?.Message });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(Guid bookId)
        {
            var result = await _library.DeleteBook(bookId);

            if (result.IsFailed && result.HasError<BookNotFoundError>())
                return NotFound(new { message = result.Errors.First().Message });

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookRequest dto)
        {
            var result = await _library.UpdateBookAsync(id, dto);

            if (result.IsFailed)
                return NotFound(result.Errors.First().Message);

            return NoContent();
        }


    }

}
