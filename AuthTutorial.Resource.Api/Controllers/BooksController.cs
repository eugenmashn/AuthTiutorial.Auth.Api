using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthTutorial.Resource.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthTutorial.Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStore store;

        public BooksController(BookStore bookStore)
        {
            this.store = bookStore;
        }


        [HttpGet]
        [Route("")]
        public IActionResult GetAvailableBooks()
        {
            return Ok(store.Books);
        }
    }
}
