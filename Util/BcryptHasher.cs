using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Warehouse.Models;

namespace Warehouse.Util {
    public class BcryptHasher : IPasswordHasher<User> {
        public string HashPassword(User user,string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, String hashedPassword, string providedPassword)
        {
            if (BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword)) {
                return PasswordVerificationResult.Success;
            }
            
            return PasswordVerificationResult.Failed;
        }
    }
}