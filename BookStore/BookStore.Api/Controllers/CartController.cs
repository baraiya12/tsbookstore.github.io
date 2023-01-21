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
    [Route("cart")]
    public class CartController :ControllerBase
    {
        CartRepository _repository = new CartRepository();

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponse<CartModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCartItems(String keyword)
        {
            ListResponse<Cart> res = _repository.GetCartItems(keyword);
            ListResponse<CartModel> listResponse = new ListResponse<CartModel>()
            {
                Results = res.Results.Select(c => new CartModel(c)),
                TotalRecords = res.TotalRecords,
            };
            return StatusCode(HttpStatusCode.OK.GetHashCode(), listResponse);
        }

        [HttpGet]
        [Route("list/{id}")]
        [ProducesResponseType(typeof(ListResponse<CartModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCartItemsId(int id)
        {
            ListResponse<Cart> res = _repository.GetCartItemsId(id);
            ListResponse<CartModel> listResponse = new ListResponse<CartModel>()
            {
                Results = res.Results.Select(c => new CartModel(c)),
                TotalRecords = res.TotalRecords,
            };
            if (res==null)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
            return StatusCode(HttpStatusCode.OK.GetHashCode(), listResponse);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Cart),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(String),(int)HttpStatusCode.BadRequest)]
        public IActionResult AddToCart(CartModel cartmodel)
        {
            if (cartmodel == null)
            {
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Enter valid Details");
            }
            Cart cart = new Cart
            {
                Id = cartmodel.Id,
                Bookid = cartmodel.BookId,
                Userid = cartmodel.UserId,
                Quantity = 1,
            };
            Cart res_cart = _repository.AddToCart(cart);
            if(res_cart==null)
            {
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Enter Valid Data");
            }
            return StatusCode(HttpStatusCode.OK.GetHashCode(), res_cart);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(String), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCart(CartModel cartmodel)
        {
            if (cartmodel == null)
            {
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Enter valid Details");
            }
            Cart cart = new Cart
            {
                Id = cartmodel.Id,
                Bookid = cartmodel.BookId,
                Userid = cartmodel.UserId,
                Quantity = cartmodel.Quantity,
            };
            Cart res_cart = _repository.UpdateCart(cart);
            if (res_cart == null)
            {
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Enter Valid Data");
            }
            return StatusCode(HttpStatusCode.OK.GetHashCode(), res_cart);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(String), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(String), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCart(int id)
        {
            if (id == 0)
            {
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Enter valid ID");
            }
            if(_repository.DeleteCart(id))
            {
                return StatusCode(HttpStatusCode.OK.GetHashCode(), "Deleted Successfully!");
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "TRY AGAIN!!");
            }
            
        }
    }
}
