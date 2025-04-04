using EuroTruck.AuthFeature.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using EuroTruck.AuthFeature.Data;
using Microsoft.EntityFrameworkCore;

namespace EuroTruck.AuthFeature.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;

        public AuthService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            try
            {
                Console.WriteLine("Checking if user exists...");

                if (await _db.Users.AnyAsync(u => u.Username == user.Username))
                {
                    Console.WriteLine("User already exists.");
                    return false;
                }

                Console.WriteLine("Hashing password...");
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                Console.WriteLine("Adding user...");
                _db.Users.Add(user);

                Console.WriteLine("Saving changes...");
                await _db.SaveChangesAsync();

                Console.WriteLine("User registered successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> LoginAsync(User user)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            return existingUser != null && BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);
        }
    }
}

//CREATE TABLE users (
//id SERIAL PRIMARY KEY,
//username TEXT NOT NULL,
//password TEXT NOT NULL
//);

