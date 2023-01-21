using System;
using BookStore.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class CartModel
    {
        public CartModel() { }
        public CartModel(Cart cart)
        {
            this.Id = cart.Id;
            this.UserId = cart.Userid;
            this.BookId = cart.Bookid;
            this.BookName = cart.Book.Name;
            this.Price = cart.Book.Price;
            this.Base64image = cart.Book.Base64image;
            this.Quantity = cart.Quantity;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string BookName { get; set; }
        public decimal Price { get; set; }
        public string Base64image { get; set; }
    }
}
