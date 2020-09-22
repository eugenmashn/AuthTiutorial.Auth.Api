using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthTutorial.Resource.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthTutorial.Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly BookStore store;

        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public OrdersController(BookStore bookStore)
        {
            this.store = bookStore;
        }


        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("")]
        public IActionResult GetOrders()
        {
            if (!store.Orders.ContainsKey(UserId))
                return Ok(Enumerable.Empty<Book>());

            var orderedBookIds = store.Orders.Single(o => o.Key == UserId).Value;
            var orderedBooks = store.Books.Where(b => orderedBookIds.Contains(b.Id));
            return Ok(orderedBooks);
        }
    }
}
