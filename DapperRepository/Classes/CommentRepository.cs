using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using PostgresTest.Domain.Models;
using PostgresTest.Repository.Interfaces;

namespace PostgresTest.DapperRepository
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
            using (var conn = new NpgsqlConnection(_strConn))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@p1", entity.Comment);
                parameters.Add("@p2", entity.IdPost);
                parameters.Add("@p3", entity.User_Name);

                conn.Execute("CALL savecomment(@p1, @p2, @p3)", parameters);
            }
        }

        public void DeleteComment(int id)
        {
            throw new NotImplementedException();
        }

        public Comments GetComment(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comments> GetComments(int idPost)
        {
            using (var conn = new NpgsqlConnection(_strConn))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@p1", idPost);

                return conn.Query<Comments>("select * from getcomments(@p1)", parameter);
            }
        }

        public void UpdateComment(Comments entity)
        {
            throw new NotImplementedException();
        }
    }
}
