using EventsDAL.Models;
using EventsDAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public class AccountService : IAccountService
    {
        public bool RegisterUser(Register userReg)
        {
            try
            {
                using(var context = new EventContext())
                {
                   var existing=  context.Users.Where(u => u.Email.Equals(userReg.Email)).FirstOrDefault();
                    if (existing!=null)
                    {
                        return false;
                    }
                    User user = new User();
                    user.Id = Guid.NewGuid();
                    user.Email = userReg.Email;
                    user.UserName = userReg.FirstName + " " + userReg.LastName;
                    if(user.Password!=null)
                    {
                        user.Password = BCrypt.Net.BCrypt.HashPassword(userReg.Password);
                    }
                    else
                    {
                        user.Password = null;
                    }
                   
                    user.UserRole = null;
                    context.Users.Add(user);
                    context.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public User AuthenticateUser(Login user)
        {
            try{
                using (var context = new EventContext())
                {
                    var existing = context.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
                    if (existing != null)
                    {
                       if(BCrypt.Net.BCrypt.Verify(user.Password, existing.Password))
                        {
                            return existing;
                        }
                       else { 
                            return null;
                        }
                    }
                    return null ;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                using (var context = new EventContext())
                {
                   var  allUsers =  context.Users.ToList();
                    return allUsers;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool SetUserRole(Guid userId, Role role)
        {
            try
            {
                using (var context = new EventContext())
                {
                    var existingUser = context.Users.Where(u=>u.Id.Equals(userId)).FirstOrDefault();
                    if (existingUser != null)
                    {
                        string query = @$"Update [dbo].[Users] Set [UserRole] = {role.GetHashCode()} where [Id] = '{userId}'";
                        context.Database.ExecuteSqlRaw(query);
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool IsUserExist(string email)
        {
            try
            {
                using (var context = new EventContext())
                {
                    var existingUser = context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
                    if (existingUser != null)
                    {
                       
                        return true;
                    }
                    return false;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Role? GetUserRole(string email)
        {
            try
            {
                using (var context = new EventContext())
                {
                    var existingUser = context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
                    if (existingUser != null)
                    {

                        return existingUser.UserRole;
                    }
                    return null;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
