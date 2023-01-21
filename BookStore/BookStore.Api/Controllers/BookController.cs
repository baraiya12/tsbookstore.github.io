using BookStore.Models.ViewModels;
using BookStore.Models.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController : ControllerBase
    {
        BookRepository _bookRepository = new BookRepository();

        [Route("list")]
        [HttpGet]
        public IActionResult GetBooks(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            var books = _bookRepository.GetBooks(pageIndex, pageSize, keyword);
            ListResponse<BookModel> listResponse = new ListResponse<BookModel>()
            {
                Results = books.Results.Select(c => new BookModel(c)),
                TotalRecords = books.TotalRecords,
            };

            return Ok(listResponse);
        }

        [Route("getbook/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(BookModel),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(String),(int)HttpStatusCode.BadRequest)]
        public IActionResult GetBook(int id)
        {
            var data = _bookRepository.GetBook(id);
            if (data == null)
            {
                return BadRequest("Data not Found!");
            }
            BookModel bookmodel = new BookModel(data);
            return Ok(bookmodel);
        }

        [Route("addbook")]
        [HttpPost]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(String), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(BookModel model)
        {
            if (model == null)
            {
                return BadRequest("Please Enter Valid Data!");
            }
            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            Book res_book  = _bookRepository.AddBook(book);
            return Ok(res_book);
        }

        [Route("deletebook/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(String), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(String), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            bool res = _bookRepository.DeleteBook(id);
            if (res)
                return Ok("Deleted Successfully..");
            else
                return BadRequest("Try Again!");
        }

        [Route("updatebook")]
        [HttpPut]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(String), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel model)
        {
            if(model == null)
            {
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Enter valid Details");
            }
            Book book = new Book {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                //Base64image = book.Base64image;
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            Book book_res = _bookRepository.UpdateBook(book);
            if (book_res == null)
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Error while Updating Data");
            else
                return StatusCode(HttpStatusCode.OK.GetHashCode(),book_res);
            
        }
    }
}
