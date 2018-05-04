using Library.BLL.Infrastructure;
using Library.DAL.Entities;
using Library.DAL.Repositories;
using Library.ViewModels.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Services
{
    public class UserService:IDisposable
    {
        private IdentityUnitOfWork _uow { get; set; }

        public UserService(IdentityUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<OperationDetails> Create(UserViewModel userVM)
        {
            ApplicationUser user = await _uow.UserManager.FindByEmailAsync(userVM.Email);
            if (user != null)
            {
                return new OperationDetails(false, "User exsist", "Email");
            }
            user = new ApplicationUser { Email = userVM.Email, UserName = userVM.Email };
            IdentityResult result = await _uow.UserManager.CreateAsync(user, userVM.Password);
            if (result.Errors.Count() > 0)
            {
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
            }

            await _uow.UserManager.AddToRoleAsync(user.Id, userVM.Role);

            var clientProfile = new ClientProfile { Id = user.Id, Address = userVM.Address, Name = userVM.Name };
            _uow.ClientManager.Create(clientProfile);
            await _uow.SaveAsync();
            return new OperationDetails(true, "Registration success", "");
        }

        public async Task<ClaimsIdentity> Authenticate(UserViewModel userVM)
        {
            ClaimsIdentity claim = null;

            ApplicationUser user = await _uow.UserManager.FindAsync(userVM.Email, userVM.Password);

            if (user != null)
                claim = await _uow.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        
        public async Task SetInitialData(UserViewModel admin, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                ApplicationRole role = await _uow.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await _uow.RoleManager.CreateAsync(role);
                }
            }
            await Create(admin);
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
