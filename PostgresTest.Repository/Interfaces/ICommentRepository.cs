using System.Collections.Generic;
using PostgresTest.Domain.Models;

namespace PostgresTest.Repository.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comments> GetComments(int idPost);
        Comments GetComment(int id);
        void CreateComment(Comments entity);
        void UpdateComment(Comments entity);
        void DeleteComment(int id);
    }
}
