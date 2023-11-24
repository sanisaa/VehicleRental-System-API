using AutoMapper;
using Dapper;
using Domain.DTO;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using Repositories.DataAccess;
using Repositories.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Implementation.Implementation
{
    public class DataAccess : IDataAccess
    {

        public static User user = new User();


        private readonly VehicleDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string DbConnection;


        public DataAccess(VehicleDbContext dbContext, IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _mapper = mapper;
            DbConnection = _configuration["connectionStrings:DBConnect"] ?? "";

        }

        //register user
        public async Task<string> CreateUser(UserDto user)
        {
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var add = _mapper.Map<User>(user);
            add.PasswordHash = passwordHash;
            add.PasswordSalt = passwordSalt;


            await _dbContext.Users.AddAsync(add);
            await _dbContext.SaveChangesAsync();
            return "ok";
        }

        //check if email is already used
        public bool isEmailAvailable(string email)
        {
            var result = false;
            using (var connection = new SqlConnection(DbConnection))
            {
                result = connection.ExecuteScalar<bool>("select count(*) from Users where Email = @email;", new { email });

            }

            return !result;
            //sending negative value if 0 then return true and if 1 then return false
        }

        //login
        public bool AuthenticateUser(string email, string password, out User? user)
        {
            var result = false;
            using (var connection = new SqlConnection(DbConnection))
            {
                result = connection.ExecuteScalar<bool>("select count(1) from Users where email=@email;", new { email });
                if (result)
                {
                    user = connection.QueryFirst<User>("select * from Users where email=@email;", new { email });
                    if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    {
                        return false;
                    }
                }
                else
                {
                    user = null;
                }
            }
            return result;
        }


        //create hashed password
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }


        //verify the hashed password
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


    }
}
