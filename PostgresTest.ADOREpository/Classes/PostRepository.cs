using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;
using PostgresTest.Domain.Models;
using PostgresTest.Repository.Interfaces;

namespace PostgresTest.ADORepository
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
            string query = "insert into posts (content, type, user_name, publish_date) values (@p1, @p2, @p3, @p4)";

            using (NpgsqlConnection conn = new NpgsqlConnection(_strConn))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    NpgsqlParameter[] parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@p1", entity.Content),
                        new NpgsqlParameter("@p2", entity.Type),
                        new NpgsqlParameter("@p3", entity.User_Name),
                        new NpgsqlParameter("@p4", entity.Publish_Date)
                    };

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePost(int id)
        {
            string query = "delete posts where id = @p1";

            using (NpgsqlConnection conn = new NpgsqlConnection(_strConn))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@p5", id));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Posts GetPost(int id)
        {
            Posts posts = new Posts();

            using (NpgsqlConnection conn = new NpgsqlConnection(_strConn))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from posts where id = @id", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@id", id));

                    using (NpgsqlDataReader dt = cmd.ExecuteReader())
                    {
                        while (dt.Read())
                        {
                            posts.Id = Convert.ToInt32(dt["id"]);
                            posts.Content = dt["content"].ToString();
                            posts.Type = dt["type"].ToString();
                            posts.User_Name = dt["user_name"].ToString();
                            posts.Publish_Date = Convert.ToDateTime(dt["publish_date"]);
                        }
                    }
                }
            }

            return posts;
        }

        public IEnumerable<Posts> GetPosts()
        {
            List<Posts> posts = new List<Posts>();

            using (NpgsqlConnection conn = new NpgsqlConnection(_strConn))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from posts", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (NpgsqlDataReader dt = cmd.ExecuteReader())
                    {
                        while (dt.Read())
                        {
                            posts.Add(
                                new Posts
                                {
                                    Id = Convert.ToInt32(dt["id"]),
                                    Content = dt["content"].ToString(),
                                    Type = dt["type"].ToString(),
                                    User_Name = dt["user_name"].ToString(),
                                    Publish_Date = Convert.ToDateTime(dt["publish_date"])
                                }
                            );
                        }
                    }
                }
            }

            return posts;
        }

        public void UpdatePost(Posts entity)
        {
            string query = "update posts set content = @p1, type = @p2, user_name = @p3, publish_date = @p4 where id = @p5";

            using (NpgsqlConnection conn = new NpgsqlConnection(_strConn))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    NpgsqlParameter[] parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@p1", entity.Content),
                        new NpgsqlParameter("@p2", entity.Type),
                        new NpgsqlParameter("@p3", entity.User_Name),
                        new NpgsqlParameter("@p4", entity.Publish_Date),
                        new NpgsqlParameter("@p5", entity.Id)
                    };

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
