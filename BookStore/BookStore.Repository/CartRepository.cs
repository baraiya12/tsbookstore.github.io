using System;
using BookStore.Models.ViewModels;
using BookStore.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class CartRepository
    {
        BookStoreContext _context = new BookStoreContext();

        public Cart AddToCart(Cart cart)
        {
            var entry = _context.Add(cart);
            _context.SaveChanges();
            return entry.Entity;
        }

        public ListResponse<Cart> GetCartItems(String keyword)
        {
            var query = _context.Carts.Include(c => c.Book).Where(c => keyword == null || c.Book.Name.ToLower().Contains(keyword)).AsQueryable();
            List<Cart> cart = query.ToList();
            return new ListResponse<Cart>()
            {
                Results = cart,
                TotalRecords = query.Count(),
            };
        }

        public ListResponse<Cart> GetCartItemsId(int id)
        {
            var query = _context.Carts.Include(c=> c.Book).Where(c => c.Userid == id).AsQueryable();
            List<Cart> cart = query.ToList();
            return new ListResponse<Cart>()
            {
                Results = cart,
                TotalRecords = query.Count(),
            };
        }

        public Cart UpdateCart(Cart cart)
        {
            var entry = _context.Update(cart);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCart(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
                return false;

            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}
