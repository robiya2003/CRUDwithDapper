using CRUDwithDapper.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics.Contracts;

namespace CRUDwithDapper.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public const string CONNECTINGSTRING = "Server=127.0.0.1;Port=5432;Database=forapi;User Id=postgres;Password=dfrt43i0";
        [HttpGet]
        public List<UserModel> GetUsers()
        {
            #region
            using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(CONNECTINGSTRING))
            {
                return npgsqlConnection.Query<UserModel>("select * from users").ToList();
            }
            #endregion
        }
        [HttpPost]
        public string PostUsers(UserModelTDO user)
        {
            #region
            string query = $"insert into users(name,phonenumber,email,password) values" +
                $"(@Name,@PhoneNumber,@Email,@Password)";
            using(NpgsqlConnection npgsqlConnection = new NpgsqlConnection(CONNECTINGSTRING))
            {
                npgsqlConnection.Execute(query, user);
            }

            return "malumot qoshildi";
            #endregion
        }
        [HttpDelete]
        public string DeleteUsers(int id_)
        {
            #region
            string query = "delete from users where id=@id";
            using(NpgsqlConnection npgsqlConnection=new NpgsqlConnection(CONNECTINGSTRING))
            {
                npgsqlConnection.Execute(query,new { id=id_ });
            }
            return $"{id_} ochirildi";
            #endregion
        }
        [HttpPut]
        public UserModel PutUsers(int id, UserModelTDO modelTDO)
        {
            #region
            string query = $"update users set " +
                $"name=@Name," +
                $"phonenumber=@PhoneNumber," +
                $"email=@Email," +
                $"password=@Password where id=@id";
            using(NpgsqlConnection npgsqlConnection =new NpgsqlConnection(CONNECTINGSTRING))
            {
                UserModel user = new UserModel()
                {
                    Id = id,
                    Name = modelTDO.Name,
                    PhoneNumber = modelTDO.PhoneNumber,
                    Email = modelTDO.Email,
                    Password = modelTDO.Password
                };
                npgsqlConnection.Execute(query,user);
                return user;
            }
            #endregion
        }
    }
}
