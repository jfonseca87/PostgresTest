using System.Collections.Generic;
using System.Linq;
using PostgresTest.Business.Interfaces;
using PostgresTest.Domain.Models;
using PostgresTest.Repository.Interfaces;

namespace PostgresTest.Business.Classess
{
    public class PostBusiness : IPostBusiness
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public PostBusiness(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public void CreatePost(Posts entity)
        {
            _postRepository.CreatePost(entity);
        }

        public void DeletePost(int id)
        {
            _postRepository.DeletePost(id);
        }

        public Posts GetPost(int id)
        {
            return _postRepository.GetPost(id) ?? new Posts();
        }

        public Posts GetCompletePost(int id)
        {
            var post = _postRepository.GetPost(id);
            post.Comments = _commentRepository.GetComments(id).ToList();

            return post;
        }

        public IEnumerable<Posts> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        public void UpdatePost(Posts entity)
        {
            _postRepository.UpdatePost(entity);
        }
    }
}
