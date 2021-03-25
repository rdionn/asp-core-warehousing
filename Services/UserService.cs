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

namespace Warehouse.Services
{
    public class UserService : IUserRoleStore<User>, IUserPasswordStore<User> {
        private readonly ApplicationContext _appContext;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationContext appContext, ILogger<UserService> logger) {
            _appContext = appContext;
            _logger = logger;
        }

        /// <summary>
        /// Gets the user identifier for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose identifier should be retrieved.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the identifier for the specified <paramref name="user"/>.</returns>
        public async Task<string> GetUserIdAsync(User user, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return null;
                }

                return user.UserName;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return null;
        }

        /// <summary>
        /// Gets the user name for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose name should be retrieved.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the name for the specified <paramref name="user"/>.</returns>
        public async Task<string> GetUserNameAsync(User user, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return null;
                }

                return user.UserName;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return null;
        }

        /// <summary>
        /// Sets the given <paramref name="userName" /> for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="userName">The user name to set.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task SetUserNameAsync(User user, string userName, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return;
                }

                user.UserName = userName;

                return;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return;
        }

        /// <summary>
        /// Gets the normalized user name for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose normalized name should be retrieved.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the normalized user name for the specified <paramref name="user"/>.</returns>
        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return "";
                }

                return user.UserName;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return "";
        }

        /// <summary>
        /// Sets the given normalized name for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="normalizedName">The normalized name to set.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken token) {
            user.UserName = normalizedName;
        }

        /// <summary>
        /// Creates the specified <paramref name="user"/> in the user store.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the creation operation.</returns>
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return IdentityResult.Failed();
                }

                _appContext.Users.Add(user);
                await _appContext.SaveChangesAsync();

                return IdentityResult.Success;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return IdentityResult.Failed();
        }

        /// <summary>
        /// Updates the specified <paramref name="user"/> in the user store.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the update operation.</returns>
        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return IdentityResult.Failed();
                }

                await _appContext.SaveChangesAsync();

                return IdentityResult.Success;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return IdentityResult.Failed();
        }

        /// <summary>
        /// Deletes the specified <paramref name="user"/> from the user store.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/> of the update operation.</returns>
        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return IdentityResult.Failed();
                }
                _appContext.Users.Remove(user);
                await _appContext.SaveChangesAsync();

                return IdentityResult.Success;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return IdentityResult.Failed();
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">The user ID to search for.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId"/> if it exists.
        /// </returns>
        public async Task<User> FindByIdAsync(string userId, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return null;
                }

                return await _appContext.Users.Where(u => (u.UserName == userId || u.Email == userId) && u.Status == "PUBLISH").SingleOrDefaultAsync();
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return null;
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified normalized user name.
        /// </summary>
        /// <param name="normalizedUserName">The normalized user name to search for.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation, containing the user matching the specified <paramref name="normalizedUserName"/> if it exists.
        /// </returns>
        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return null;
                }

                return await _appContext.Users.Where(u => (u.UserName == normalizedUserName || u.Email == normalizedUserName) && u.Status == "PUBLISH").SingleOrDefaultAsync();
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return null;
        }

        /// <summary>
        /// Add the specified <paramref name="user"/> to the named role.
        /// </summary>
        /// <param name="user">The user to add to the named role.</param>
        /// <param name="roleName">The name of the role to add the user to.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task AddToRoleAsync(User user, string roleName, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return;
                }

                var role = new Role() {
                    UserId = user.Id,
                    Name = roleName
                };

                _appContext.Roles.Add(role);

                await _appContext.SaveChangesAsync();
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }
        }

        /// <summary>
        /// Remove the specified <paramref name="user"/> from the named role.
        /// </summary>
        /// <param name="user">The user to remove the named role from.</param>
        /// <param name="roleName">The name of the role to remove.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return;
                }

                var role = await _appContext.Roles.Where(r => r.Name == roleName && r.UserId == user.Id).SingleOrDefaultAsync();

                if (role != null) {
                    _appContext.Roles.Remove(role);
                    await _appContext.SaveChangesAsync();
                }
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return;
        }

        /// <summary>
        /// Gets a list of role names the specified <paramref name="user"/> belongs to.
        /// </summary>
        /// <param name="user">The user whose role names to retrieve.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing a list of role names.</returns>
        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return new List<String>();
                }

                var roles = await _appContext.Roles.Where(r => r.UserId == user.Id).ToListAsync();
                return roles.Select(r => r.Name).ToList();
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return new List<String>();
        }

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="user"/> is a member of the given named role.
        /// </summary>
        /// <param name="user">The user whose role membership should be checked.</param>
        /// <param name="roleName">The name of the role to be checked.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation, containing a flag indicating whether the specified <paramref name="user"/> is
        /// a member of the named role.
        /// </returns>
        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return false;
                }

                var roles = await _appContext.Roles.Where(r => r.UserId == user.Id && r.Name == roleName).SingleOrDefaultAsync();
                return roles != null ? true : false;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return false;
        }

        /// <summary>
        /// Returns a list of Users who are members of the named role.
        /// </summary>
        /// <param name="roleName">The name of the role whose membership should be returned.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation, containing a list of users who are in the named role.
        /// </returns>
        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return new List<User>();
                }

                var roles = await _appContext.Roles.Where(r => r.Name == roleName).Include(r => r.User).ToListAsync();
                return roles.Select(r => r.User).ToList();
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return new List<User>();;
        }

        /// <summary>
        /// Sets the password hash for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose password hash to set.</param>
        /// <param name="passwordHash">The password hash to set.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return;
                }

                user.PasswordHash = passwordHash;
                await _appContext.SaveChangesAsync();
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }
        }

        /// <summary>
        /// Gets the password hash for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose password hash to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, returning the password hash for the specified <paramref name="user"/>.</returns>
        public async Task<string> GetPasswordHashAsync(User user, CancellationToken token) {
            try {
                if (token.IsCancellationRequested) {
                    return null;
                }

                return user.PasswordHash;
            } catch (Exception e) {
                _logger.LogError(e, "Get User Id Failed");
            }

            return null;
        }

        /// <summary>
        /// Gets a flag indicating whether the specified <paramref name="user"/> has a password.
        /// </summary>
        /// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation, returning true if the specified <paramref name="user"/> has a password
        /// otherwise false.
        /// </returns>
        public async Task<bool> HasPasswordAsync(User user, CancellationToken token) {
            return true;
        }

        public void Dispose() {
            // Disposed By Services It Self
        }
    }
}