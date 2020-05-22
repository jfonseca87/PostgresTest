using System.Collections.Generic;
using PostgresTest.Domain.Models;

namespace PostgresTest.Repository.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Posts> GetPosts();
        Posts GetPost(int id);
        void CreatePost(Posts entity);
        void UpdatePost(Posts entity);
        void DeletePost(int id);
    }
}
