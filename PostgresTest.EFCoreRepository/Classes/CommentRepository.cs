using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PostgresTest.Domain.Models;
using PostgresTest.Repository.Interfaces;

namespace PostgresTest.EFCoreRepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly PgContext _db;

        public CommentRepository(PgContext db)
        {
            _db = db;
        }

        public void CreateComment(Comments entity)
        {
            _db.Add(entity);
            SaveChanges();
        }

        public void DeleteComment(int id)
        {
            Comments comment = new Comments
            {
                Id = id
            };

            _db.Comments.Remove(comment);
            SaveChanges();
        }

        public Comments GetComment(int id)
        {
            return _db.Comments.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Comments> GetComments(int idPost)
        {
            return _db.Comments.Where(x => x.IdPost == idPost).ToList();
        }

        public void UpdateComment(Comments entity)
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
