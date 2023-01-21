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
    public class RoleRepository
    {
        BookStoreContext _context = new BookStoreContext();

        public ListResponse<Role> getAllRoles() {
            var list =  _context.Roles.ToList();
            int totalRecords = list.Count();
            return new ListResponse<Role>()
            {
                Results = list,
                TotalRecords = totalRecords,
            };
        }
        
    }
}
