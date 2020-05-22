using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PostgresTest.Domain.Models;
using PostgresTest.Repository.Interfaces;

namespace PostgresTest.EFCoreRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly PgContext _db;

        public PostRepository(PgContext db)
        {
            _db = db;
        }

        public void CreatePost(Posts entity)
        {
            _db.Posts.Add(entity);
            SaveChanges();
        }

        public void DeletePost(int id)
        {
            Posts post = new Posts
            {
                Id = id
            };

            _db.Remove(post);
            SaveChanges();
        }

        public Posts GetPost(int id)
        {
            return _db.Posts.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Posts> GetPosts()
        {
            return _db.Posts.AsEnumerable();
        }

        public void UpdatePost(Posts entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
