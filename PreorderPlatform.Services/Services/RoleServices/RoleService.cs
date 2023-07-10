using AutoMapper;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.RoleRepositories;
using PreorderPlatform.Service.Services.RoleServices;
using PreorderPlatform.Service.ViewModels.Role;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.RoleServices
{
    internal class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleDetailViewModel>> GetRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();
                return _mapper.Map<List<RoleDetailViewModel>>(roles);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching roles.", ex);
            }
        }

        public async Task<RoleDetailViewModel> GetRoleByIdAsync(int id)
        {
            try
            {
                var role = await _roleRepository.GetByIdAsync(id);

                if (role == null)
                {
                    throw new NotFoundException($"Role with ID {id} was not found.");
                }

                return _mapper.Map<RoleDetailViewModel>(role);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while fetching role with ID {id}.", ex);
            }
        }

        public async Task<RoleDetailViewModel> CreateRoleAsync(RoleCreateViewModel model)
        {
            try
            {
                var role = _mapper.Map<Role>(model);
                await _roleRepository.CreateAsync(role);
                return _mapper.Map<RoleDetailViewModel>(role);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the role.", ex);
            }
        }

        public async Task UpdateRoleAsync(RoleDetailViewModel model)
        {
            try
            {
                var role = await _roleRepository.GetByIdAsync(model.Id);
                role = _mapper.Map(model, role);
                await _roleRepository.UpdateAsync(role);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while updating role with ID {model.Id}.", ex);
            }
        }

        public async Task DeleteRoleAsync(int id)
        {
            try
            {
                var role = await _roleRepository.GetByIdAsync(id);
                await _roleRepository.DeleteAsync(role);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while deleting role with ID {id}.", ex);
            }
        }
    }
}