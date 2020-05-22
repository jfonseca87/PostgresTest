using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Npgsql;
using PostgresTest.Domain.Models;
using PostgresTest.Repository.Interfaces;

namespace PostgresTest.ADORepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string _strConn;

        public CommentRepository(IOptions<SettingsValues> options)
        {
            _strConn = options.Value.ConnectionString;
        }

        public void CreateComment(Comments entity)
        {
            string query = "insert into comments (comment, idpost, user_name, comment_date) values (@p1, @p2, @p3, @p4)";

            using (NpgsqlConnection conn = new NpgsqlConnection(_strConn))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    NpgsqlParameter[] parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@p1", entity.Comment),
                        new NpgsqlParameter("@p2", entity.IdPost),
                        new NpgsqlParameter("@p3", entity.User_Name),
                        new NpgsqlParameter("@p4", entity.Comment_Date)
                    };

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteComment(int id)
        {
            string query = "delete comments where id = @p1";

            using (NpgsqlConnection conn = new NpgsqlConnection(_strConn))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@p1", id));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Comments GetComment(int id)
        {
            Comments comment = new Comments();
            string query = "select * from comments where id = @p1";

            using (NpgsqlConnection conn = new NpgsqlConnection(_strConn))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@p1", id));
                    conn.Open();

                    using (NpgsqlDataReader dt = cmd.ExecuteReader())
                    {
                        while (dt.Read())
                        {

                            comment.Id = Convert.ToInt32(dt["id"]);
                            comment.Comment = dt["comment"].ToString();
                            comment.IdPost = Convert.ToInt32(dt["idpost"]);
                            comment.User_Name = dt["user_name"].ToString();
                            comment.Comment_Date = Convert.ToDateTime(dt["comment_date"]);

                        }
                    }
                }
            }

            return comment;
        }

        public IEnumerable<Comments> GetComments(int idPost)
        {
            List<Comments> comments = new List<Comments>();
            string query = "select * from comments where idpost = @p1";

            using (NpgsqlConnection conn = new NpgsqlConnection(_strConn))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new NpgsqlParameter("@p1", idPost));
                    conn.Open();

                    using (NpgsqlDataReader dt = cmd.ExecuteReader())
                    {
                        while (dt.Read())
                        {
                            comments.Add(new Comments
                            {
                                Id = Convert.ToInt32(dt["id"]),
                                Comment = dt["comment"].ToString(),
                                IdPost = Convert.ToInt32(dt["idpost"]),
                                User_Name = dt["user_name"].ToString(),
                                Comment_Date = Convert.ToDateTime(dt["comment_date"])
                            });
                        }
                    }
                }
            }

            return comments;
        }

        public void UpdateComment(Comments entity)
        {
            string query = "update comments set comment = @p1, user_name = @p2, comment_date = @p3 where id = @p4";

            using (NpgsqlConnection conn = new NpgsqlConnection(_strConn))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    NpgsqlParameter[] parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("@p1", entity.Comment),
                        new NpgsqlParameter("@p2", entity.IdPost),
                        new NpgsqlParameter("@p3", entity.User_Name),
                        new NpgsqlParameter("@p4", entity.Id)
                    };

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
