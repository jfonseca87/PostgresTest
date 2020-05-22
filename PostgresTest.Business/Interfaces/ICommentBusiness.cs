using System.Collections.Generic;
using PostgresTest.Domain.Models;

namespace PostgresTest.Business.Interfaces
{
    public interface ICommentBusiness
    {
        IEnumerable<Comments> GetComments(int idPost);
        Comments GetComment(int id);
        void CreateComment(Comments entity);
        void UpdateComment(Comments entity);
        void DeleteComment(int id);
    }
}
