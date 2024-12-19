using Doctors_Web_Forum.BLL.IServices;
using Doctors_Web_Forum.DAL.Data;
using Doctors_Web_Forum.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors_Web_Forum.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataDBContext _dataDBContext;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DataDBContext dataDBContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dataDBContext = dataDBContext;
        }

        public async Task<(IEnumerable<User> users, Paginate pager)> GetAllUsersAsync(int pg, int pageSize = 5, string searchTerm = "")
        {
            if (pg <= 0) pg = 1;

            var usersQuery = _dataDBContext.Users.AsQueryable();

            // Tìm kiếm theo từ khóa (nếu có)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                usersQuery = usersQuery.Where(u => u.UserName.Contains(searchTerm) || u.Email.Contains(searchTerm));
            }

            usersQuery = usersQuery.OrderByDescending(u => u.Id);

            int recsCount = await usersQuery.CountAsync();

            var pager = new Paginate(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            // Lấy dữ liệu người dùng với phân trang
            var data = await usersQuery.Skip(recSkip).Take(pageSize).ToListAsync();

            return (data, pager);
        }


        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> CreateUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null) return false;

            existingUser.FullName = user.FullName;
            existingUser.Address = user.Address;
            existingUser.Status = user.Status;
            existingUser.LastLogin = user.LastLogin;

            var result = await _userManager.UpdateAsync(existingUser);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> AddUserToRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Enumerable.Empty<string>();

            return await _userManager.GetRolesAsync(user);
        }

        
    }
}
