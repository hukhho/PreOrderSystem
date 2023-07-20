using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.Enum.User;
using PreorderPlatform.Entity.Repositories.RoleRepositories;
using PreorderPlatform.Entity.Repositories.UserRepositories;
using PreorderPlatform.Entity.Repositories.UserRepository;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.Helpers;
using PreorderPlatform.Service.Utility;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.User.Request;
using PreorderPlatform.Service.ViewModels.User.Response;

namespace PreorderPlatform.Service.Services.UserServices
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> GetUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return _mapper.Map<List<UserResponse>>(users);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching users.", ex);
            }
        }

        //GetAllUsersWithRoleAndBusinessAsync
        public async Task<List<UserResponse>> GetAllUsersWithRoleAndBusinessAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersWithRoleAndBusinessAsync();
                return _mapper.Map<List<UserResponse>>(users);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching users.", ex);
            }
        }

        public async Task<UserResponse> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);

                if (user == null)
                {
                    throw new NotFoundException($"User with ID {id} was not found.");
                }

                return _mapper.Map<UserResponse>(user);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while fetching user with ID {id}.",
                    ex
                );
            }
        }

        public async Task<UserResponse> GetUserWithRoleAndBusinessByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserWithRoleAndBusinessByIdAsync(id);

                if (user == null)
                {
                    throw new NotFoundException($"User with ID {id} was not found.");
                }

                return _mapper.Map<UserResponse>(user);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while fetching user with ID {id}.",
                    ex
                );
            }
        }

        private string GenerateActionToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        public async Task<UserResponse> ConfirmEmail(string token, ActionType actionType)
        {
            var user = await _userRepository.GetUserByActionTokenAsync(token, actionType);
            if (user == null)
            {
                throw new ServiceException("Invalid token.");
            }
            DateTime nowUtcPlus7 = DateTimeUtcPlus7.Now;

            if (nowUtcPlus7 > user.ActionTokenExpiration)
            {
                throw new ServiceException("Token has expired.");
            }

            if(user.Status != UserStatus.Suspended)
            {
                user.Status = UserStatus.Active;
            }
            if(actionType == ActionType.AccountActivation)
            {
                user.ActionToken = null;
                user.ActionTokenType = null;
                user.ActionTokenExpiration = null;
            }
            else if (actionType== ActionType.PasswordReset)
            {
                
            }
            user.ActionToken = null;
            user.ActionTokenType = null;
            user.ActionTokenExpiration = null;
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<bool> IsTokenValid(string token, ActionType actionType)
        {
            var user = await _userRepository.GetUserByActionTokenAsync(token, actionType);
            if (user == null)
            {
                return false;
            }
            DateTime nowUtcPlus7 = DateTimeUtcPlus7.Now;

            return user.ActionTokenExpiration > nowUtcPlus7;
        }
        public async Task<User> RegisterUser(UserCreateRequest model)
        {
            try
            {
                var role = await _roleRepository.GetByNameAsync(model.RoleName);
                if (role == null)
                {
                    throw new ServiceException($"Role {model.RoleName} does not exist.");
                } 

                var user = _mapper.Map<User>(model);
                user.RoleId = role.Id;
                user.Status = UserStatus.Inactive;
                user.ActionToken = GenerateActionToken();
                user.ActionTokenExpiration = DateTime.UtcNow.AddHours(24);
                user.ActionTokenType = ActionType.AccountActivation;

                await _userRepository.CreateAsync(user);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ServiceException("An error occurred while creating the user.", ex);
            }
        }


        public async Task<UserResponse> ResendActiveMail(UserForgotPasswordRequest model)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    throw new ServiceException($"User with email {model.Email} does not exist.");
                }

                user.ActionToken = GenerateActionToken();
                user.ActionTokenExpiration = DateTime.UtcNow.AddHours(24);
                user.ActionTokenType = ActionType.AccountActivation;

                await _userRepository.UpdateAsync(user);

                return _mapper.Map<UserResponse>(user);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the user.", ex);
            }
        }

        public async Task<User> ForgotPassword(UserForgotPasswordRequest model)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    throw new ServiceException($"User with email {model.Email} does not exist.");
                }

                user.ActionToken = GenerateActionToken();
                user.ActionTokenExpiration = DateTime.UtcNow.AddHours(24);
                user.ActionTokenType = ActionType.PasswordReset;

                await _userRepository.UpdateAsync(user);


                return user;
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the user.", ex);
            }
        }

        public async Task<UserResponse> CreateUserAsync(UserCreateRequest model)
        {
            try
            {
                var role = await _roleRepository.GetByNameAsync(model.RoleName);
                if (role == null)
                {
                    throw new ServiceException($"Role {model.RoleName} does not exist.");
                }

                var user = _mapper.Map<User>(model);
                user.RoleId = role.Id;
                await _userRepository.CreateAsync(user);
                return _mapper.Map<UserResponse>(user);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the user.", ex);
            }
        }

        public async Task UpdateUserAsync(UserUpdateRequest model)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(model.Id);
                user = _mapper.Map(model, user);
                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while updating user with ID {model.Id}.",
                    ex
                );
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                await _userRepository.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while deleting user with ID {id}.",
                    ex
                );
            }
        }

        public async Task<(IList<UserResponse> users, int totalItems)> GetAsync(
            PaginationParam<UserEnum.UserSort> paginationModel,
            UserSearchRequest filterModel
        )
        {
            try
            {
                var query = _userRepository.Table;

                query = query.GetWithSearch(filterModel); //search

                // Calculate the total number of items before applying pagination
                int totalItems = await query.CountAsync();

                query = query
                    .GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder) //sort
                    .GetWithPaging(paginationModel.Page, paginationModel.PageSize); // pagination

                var userList = await query.ToListAsync(); // Call ToListAsync here

                // Map the userList to a list of UserResponse objects
                var result = _mapper.Map<List<UserResponse>>(userList);

                return (result, totalItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
                throw new ServiceException("An error occurred while fetching users.", ex);
            }
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            try
            {
                var result = await _userRepository.IsEmailUnique(email);

                return result;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while checking uniqueness of email {email}.",
                    ex
                );
            }
        }

        public async Task<bool> IsPhoneUniqueAsync(string phone)
        {
            try
            {
                var result = await _userRepository.IsPhoneUnique(phone);

                return result;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while checking the uniqueness of phone number {phone}.",
                    ex
                );
            }
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            return user;
        }
        public async Task UpdateUserPasswordAsync(User user, string newPassword)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                // Save the changes to the database
                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the password update process.
                throw new ServiceException($"Error updating user password: {ex.Message}", ex);
            }
        }
        public string GeneratePasswordResetToken()
        {
            // Generate a random 6-digit token
            Random random = new Random();
            int tokenValue = random.Next(100000, 999999);
            return random.Next(100000, 999999).ToString();
        }
    }
}
