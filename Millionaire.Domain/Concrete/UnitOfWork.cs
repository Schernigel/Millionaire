using System;
using Millionaire.Domain.Entities;

namespace Millionaire.Domain.Concrete
{
    public class UnitOfWork : IDisposable
    {
        private EFDbContext context = new EFDbContext();
        private GenericRepository<User> _userRepository;
        private GenericRepository<GameQuestion> _gameQuestionRepository;
        private GenericRepository<UserStatistics> _userStatRepository;
        private GenericRepository<Results> _resultRepository;
        private GenericRepository<TotalUserResult> _totalRepository;


        public GenericRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(context);
                }
                return _userRepository;
            }
        }


        public GenericRepository<GameQuestion> GameQuestionRepository
        {
            get
            {
                if (_gameQuestionRepository == null)
                {
                    _gameQuestionRepository = new GenericRepository<GameQuestion>(context);
                }
                return _gameQuestionRepository;
            }
        }
        public GenericRepository<UserStatistics> UserStatRepository
        {
            get
            {
                if (_userStatRepository == null)
                {
                    _userStatRepository = new GenericRepository<UserStatistics>(context);
                }
                return _userStatRepository;
            }
        }
        public GenericRepository<Results> ResultsRepository
        {
            get
            {
                if (_resultRepository == null)
                {
                    _resultRepository = new GenericRepository<Results>(context);
                }
                return _resultRepository;
            }
        }
        public GenericRepository<TotalUserResult> TotalRepository
        {
            get
            {
                if (_totalRepository == null)
                {
                    _totalRepository = new GenericRepository<TotalUserResult>(context);
                }
                return _totalRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }


        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
