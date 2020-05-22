using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using PostgresTest.Domain.Models;
using PostgresTest.Repository.Interfaces;

namespace PostgresTest.DapperRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly string _strConn;

        public PostRepository(IOptions<SettingsValues> options)
        {
            _strConn = options.Value.ConnectionString;
        }

        public void CreatePost(Posts entity)
        {
            using (var conn = new NpgsqlConnection(_strConn))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@p1", entity.Content);
                parameters.Add("@p2", entity.Type);
                parameters.Add("@p3", entity.User_Name);

                conn.Execute("CALL savepost(@p1, @p2, @p3)", parameters);
            }
        }

        public void DeletePost(int id)
        {
            using (var conn = new NpgsqlConnection(_strConn))
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@p1", id);

                conn.Execute("CALL deletepost(@p1)", parameter);
            }
        }

        public Posts GetPost(int id)
        {
            using (var conn = new NpgsqlConnection(_strConn))
            {
                return conn.Query<Posts>($"select * from getpost({id})").FirstOrDefault();
            }
        }

        public IEnumerable<Posts> GetPosts()
        {
            using (var conn = new NpgsqlConnection(_strConn))
            {
                return conn.Query<Posts>("select * from getPosts()");
            }
        }

        public void UpdatePost(Posts entity)
        {
            using (var conn = new NpgsqlConnection(_strConn))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@p1", entity.Id);
                parameters.Add("@p2", entity.Content);
                parameters.Add("@p3", entity.Type);
                parameters.Add("@p4", entity.User_Name);

                conn.Execute("CALL updateost(@p1, @p2, @p3, @p4)", parameters);
            }
        }
    }
}
