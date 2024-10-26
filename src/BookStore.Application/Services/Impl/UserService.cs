using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUserAsync(RegisterDto account)
        {
            if (await _unitOfWork.Users.ExistsByEmailAsync(account.Email.Trim())) return false;
            await AddUser(account);
            return true;
        }

        public async Task AddUser(RegisterDto account)
        {
            var user = _mapper.Map<User>(account);
            user.RoleId = (long)RoleEnum.USER;
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
    }
}
