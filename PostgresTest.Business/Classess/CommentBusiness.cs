using System.Collections.Generic;
using PostgresTest.Business.Interfaces;
using PostgresTest.Domain.Models;
using PostgresTest.Repository.Interfaces;

namespace PostgresTest.Business.Classess
{
    public class CommentBusiness : ICommentBusiness
    {
        private readonly ICommentRepository _commentRepository;

        public CommentBusiness(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void CreateComment(Comments entity)
        {
            _commentRepository.CreateComment(entity);
        }

        public void DeleteComment(int id)
        {
            _commentRepository.DeleteComment(id);
        }

        public Comments GetComment(int id)
        {
            return _commentRepository.GetComment(id);
        }

        public IEnumerable<Comments> GetComments(int idPost)
        {
            return _commentRepository.GetComments(idPost);
        }

        public void UpdateComment(Comments entity)
        {
            _commentRepository.UpdateComment(entity);
        }
    }
}
