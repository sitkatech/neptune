﻿using System;
using System.Linq;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hippocamp.API.Services
{
    public class ImpersonationService
    {
        private readonly HippocampDbContext _dbContext;
        private static IWebHostEnvironment _environment => new HttpContextAccessor().HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
        private readonly ILogger<ImpersonationService> _logger;
        
        public ImpersonationService(HippocampDbContext dbContext, ILogger<ImpersonationService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public UserDto ImpersonateUser(HttpContext httpContext, UserDto userToImpersonate)
        {
            var currentUser = GetOriginalUserFromHttpContext(httpContext);

            _logger.LogInformation($"UserID: {currentUser.UserID} is impersonating UserID: {userToImpersonate.UserID}");

            // Do nothing in production
            if (_environment.IsProduction())
            {
                return currentUser;
            }

            var user = _dbContext.Users
                .Single(x => x.UserID == currentUser.UserID);

            user.ImpersonatedUserGuid = userToImpersonate.UserGuid;
            user.UpdateDate = DateTime.UtcNow;

            _dbContext.SaveChanges();
            _dbContext.Entry(user).Reload();

            return User.GetByUserGuid(_dbContext, (Guid) user.ImpersonatedUserGuid);
        }
        
        public UserDto StopImpersonation(HttpContext httpContext)
        {
            var originalUser = GetOriginalUserFromHttpContext(httpContext);

            _logger.LogInformation($"UserID: {originalUser.UserID} stopped impersonation");

            var user = _dbContext.Users
                .Single(x => x.UserID == originalUser.UserID);

            user.ImpersonatedUserGuid = null;
            user.UpdateDate = DateTime.UtcNow;

            _dbContext.SaveChanges();
            _dbContext.Entry(user).Reload();
            return User.GetByUserID(_dbContext, originalUser.UserID);
        }

        public static UserDto RetrieveImpersonatedUserIfImpersonating(HippocampDbContext dbContext, UserDto currentUser)
        {
            if (_environment.IsProduction() || currentUser.ImpersonatedUserGuid == null)
            {
                return currentUser;
            }

            if (currentUser.ImpersonatedUserGuid != null)
            {
                return User.GetByUserGuid(dbContext, (Guid)currentUser.ImpersonatedUserGuid);
            }

            return currentUser;
        }

        private UserDto GetOriginalUserFromHttpContext(HttpContext httpContext)
        {
            var claimsPrincipal = httpContext.User;
            if (!claimsPrincipal.Claims.Any())
            {
                return null;
            }

            var userGuid = Guid.Parse(claimsPrincipal.Claims.Single(c => c.Type == "sub").Value);
            var keystoneUser = Hippocamp.EFModels.Entities.User.GetByUserGuid(_dbContext, userGuid);
            return keystoneUser;
        }

    }
}