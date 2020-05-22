using System.Collections.Generic;
using PostgresTest.Domain.Models;

namespace PostgresTest.Business.Interfaces
{
    public interface IPostBusiness
    {
        IEnumerable<Posts> GetPosts();
        Posts GetPost(int id);
        Posts GetCompletePost(int id);
        void CreatePost(Posts entity);
        void UpdatePost(Posts entity);
        void DeletePost(int id);
    }
}
