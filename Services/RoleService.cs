using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Warehouse.Models;

namespace Warehouse.Services {
    public class RoleService : IRoleStore<Role> {
        private readonly ApplicationContext _appContext;
        private readonly ILogger<UserService> _logger;

        public RoleService(ApplicationContext appContext, ILogger<UserService> logger) {
            _appContext = appContext;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to create in the store.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return null;
                }

                _appContext.Roles.Add(role);
                await _appContext.SaveChangesAsync();

                return IdentityResult.Success;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return IdentityResult.Failed();
        }

        /// <summary>
        /// Updates a role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to update in the store.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return null;
                }

                await _appContext.SaveChangesAsync();

                return IdentityResult.Success;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return IdentityResult.Failed();
        }

        /// <summary>
        /// Deletes a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to delete from the store.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return null;
                }

                _appContext.Roles.Remove(role);
                await _appContext.SaveChangesAsync();

                return IdentityResult.Success;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return IdentityResult.Failed();
        }

        /// <summary>
        /// Gets the ID for a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose ID should be returned.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that contains the ID of the role.</returns>
        public Task<string> GetRoleIdAsync(Role role, CancellationToken token) {
            return Task.FromResult(role.Name);
        }

        /// <summary>
        /// Gets the name of a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose name should be returned.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that contains the name of the role.</returns>
        public Task<string> GetRoleNameAsync(Role role, CancellationToken token) {
            return Task.FromResult(role.Name);
        }

        /// <summary>
        /// Sets the name of a role in the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose name should be set.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task SetRoleNameAsync(Role role, string roleName, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return;
                }

                role.Name = roleName;
                await _appContext.SaveChangesAsync();
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }
        }

        /// <summary>
        /// Get a role's normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose normalized name should be retrieved.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that contains the name of the role.</returns>
        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken token) {
            return Task.FromResult(role.Name);
        }

        /// <summary>
        /// Set a role's normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose normalized name should be set.</param>
        /// <param name="normalizedName">The normalized name to set</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken token) {
            return Task.CompletedTask;
        }


        /// <summary>
        /// Finds the role who has the specified ID as an asynchronous operation.
        /// </summary>
        /// <param name="roleId">The role ID to look for.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that result of the look up.</returns>
        public async Task<Role> FindByIdAsync(string roleId, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return null;
                }

                return await _appContext.Roles.Where(r => r.Name == roleId).SingleOrDefaultAsync();
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return null;
        }

        /// <summary>
        /// Finds the role who has the specified normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="normalizedRoleName">The normalized role name to look for.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that result of the look up.</returns>
        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return null;
                }

                return await _appContext.Roles.Where(r => r.Name == normalizedRoleName).SingleOrDefaultAsync();
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return null;
        }

        public void Dispose() {
            // Dispose Self
        }
    }
}