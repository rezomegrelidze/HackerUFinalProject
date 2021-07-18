using System;
using System.Collections.Generic;
using System.Linq;
using FlightsSystem.Core;
using FlightsSystem.Core.DAL;
using FlightsSystem.Core.Helpers;

namespace FlightsSystem.Web.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly FlightsSystemContext _db;
        private readonly List<UserModel> users = new List<UserModel>();

        public UserRepository(FlightsSystemContext db)
        {
            _db = db;
        }

        public UserModel GetUser(UserModel userModel)
        {
            var role = userModel.Role;
            userModel.Password = PasswordHasher.HashSHA1(userModel.Password);

            switch (role)
            {
                case "Administrator":
                {
                    var admin = _db.Administrators
                        .SingleOrDefault(a => a.UserName == userModel.UserName 
                                              && a.Password == userModel.Password);
                    if (admin != null)
                        return new UserModel()
                        {
                            UserName = admin.UserName, Password = admin.Password,
                            Role = "Administrator"
                        };
                    throw new InvalidOperationException("Not Such user exists");
                }
                case "Customer":
                {
                    var customer = _db.Customers
                        .SingleOrDefault(a => a.UserName == userModel.UserName
                                              && a.Password == userModel.Password);
                    if (customer != null)
                        return new UserModel()
                        {
                            UserName = customer.UserName,
                            Password = customer.Password,
                            Role = "Customer"
                        };
                    throw new InvalidOperationException("Not Such user exists");
                }
                case "AirlineCompany":
                {
                    var airlineCompany = _db.AirlineCompanies
                        .SingleOrDefault(a => a.UserName == userModel.UserName
                                              && a.Password == userModel.Password);
                    if (airlineCompany != null)
                        return new UserModel()
                        {
                            UserName = airlineCompany.UserName,
                            Password = airlineCompany.Password,
                            Role = "AirlineCompany"
                        };
                    throw new InvalidOperationException("Not Such user exists");
                }
                default:
                    throw new InvalidOperationException("No Such user exists");
            }
        }

        public Administrator GetAdministrator(UserModel userModel)
        {
            return _db.Administrators.SingleOrDefault(a =>
                       a.UserName == userModel.UserName && a.Password == userModel.Password) ?? 
                   throw new InvalidOperationException("No such administrator found");
        }

        public Customer GetCustomer(UserModel userModel)
        {
            return _db.Customers.SingleOrDefault(c =>
                       c.UserName == userModel.UserName && c.Password == userModel.Password) ??
                   throw new InvalidOperationException("No such customer found");
        }

        public AirlineCompany GetAirline(UserModel userModel)
        {
            return _db.AirlineCompanies.SingleOrDefault(a =>
                       a.UserName == userModel.UserName && a.Password == userModel.Password) ??
                   throw new InvalidOperationException("No such airline found");
        }
    }
}