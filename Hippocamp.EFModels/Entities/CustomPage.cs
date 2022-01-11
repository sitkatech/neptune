
using Hippocamp.Models.DataTransferObjects;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hippocamp.EFModels.Entities
{
    public partial class CustomPage
    {
        public static IEnumerable<CustomPageDto> List(HippocampDbContext dbContext)
        {
            return GetCustomPagesImpl(dbContext)
                .Select(x => x.AsDto()).AsEnumerable();
        }

        public static List<CustomPageWithRolesDto> ListWithRoles(HippocampDbContext dbContext)
        {
            var customPages = GetCustomPagesImpl(dbContext)
                .Select(x => x.AsDto()).ToList();

            var roles = dbContext.Roles;

            var customPagesWithRoles = new List<CustomPageWithRolesDto>();

            foreach (var customPage in customPages)
            {
                var customPageRoleIDs = dbContext.CustomPageRoles
                    .Where(x => x.CustomPageID == customPage.CustomPageID)
                    .Select(x => x.RoleID);

                var customPageWithRoles = new CustomPageWithRolesDto()
                {
                    CustomPageID = customPage.CustomPageID,
                    CustomPageDisplayName = customPage.CustomPageDisplayName,
                    CustomPageVanityUrl = customPage.CustomPageVanityUrl,
                    CustomPageContent = customPage.CustomPageContent,
                    SortOrder = customPage.SortOrder,
                    MenuItem = customPage.MenuItem,
                    ViewableRoles = roles
                        .Where(x => customPageRoleIDs.Contains(x.RoleID))
                        .Select(x => x.AsDto()).ToList()
                };

                customPagesWithRoles.Add(customPageWithRoles);
            }

            return customPagesWithRoles;
        }

        private static IQueryable<CustomPage> GetCustomPagesImpl(HippocampDbContext dbContext)
        {
            return dbContext.CustomPages
                .Include(x => x.MenuItem)
                .Include(x => x.CustomPageRoles)
                .ThenInclude(x => x.Role)
                .AsNoTracking();
        }

        public static CustomPageDto GetByCustomPageID(HippocampDbContext dbContext, int customPageID)
        {
            var customPage = GetCustomPagesImpl(dbContext)
                .SingleOrDefault(x => x.CustomPageID == customPageID);

            return customPage?.AsDto();
        }

        public static CustomPageDto GetByCustomPageVanityURL(HippocampDbContext dbContext, string customPageVanityURL)
        {
            var customPage = GetCustomPagesImpl(dbContext)
                .SingleOrDefault(x => x.CustomPageVanityUrl == customPageVanityURL);

            return customPage?.AsDto();
        }

        public static CustomPageDto UpdateCustomPage(HippocampDbContext dbContext, CustomPage customPage,
            CustomPageUpsertDto customPageUpsertDto)
        {
            // null check occurs in calling endpoint method.
            dbContext.CustomPageRoles.RemoveRange(
                dbContext.CustomPageRoles.Where(x => x.CustomPageID == customPage.CustomPageID));
            customPage.CustomPageDisplayName = customPageUpsertDto.CustomPageDisplayName;
            customPage.CustomPageVanityUrl = customPageUpsertDto.CustomPageVanityUrl;
            customPage.CustomPageContent = customPageUpsertDto.CustomPageContent;
            customPage.MenuItemID = customPageUpsertDto.MenuItemID;

            AddViewableRolesForCustomPage(dbContext, customPage, customPageUpsertDto);

            dbContext.SaveChanges();
            dbContext.Entry(customPage).Reload();
            return GetByCustomPageID(dbContext, customPage.CustomPageID);
        }

        private static void AddViewableRolesForCustomPage(HippocampDbContext dbContext, CustomPage customPage,
            CustomPageUpsertDto customPageUpsertDto)
        {
            // Add admin viewable to all new pages
            if (!customPageUpsertDto.ViewableRoleIDs.Contains((int)RoleEnum.Admin))
            {
                customPageUpsertDto.ViewableRoleIDs.Add((int)RoleEnum.Admin);
            }

            foreach (var roleID in customPageUpsertDto.ViewableRoleIDs)
            {
                var customPageRole = new CustomPageRole()
                {
                    CustomPageID = customPage.CustomPageID,
                    RoleID = roleID
                };
                dbContext.CustomPageRoles.Add(customPageRole);
            }
        }

        public static CustomPageDto CreateNewCustomPage(HippocampDbContext dbContext,
            CustomPageUpsertDto customPageUpsertDto)
        {
            if (customPageUpsertDto == null)
            {
                return null;
            }

            var customPage = new CustomPage()
            {
                CustomPageDisplayName = customPageUpsertDto.CustomPageDisplayName,
                CustomPageVanityUrl = customPageUpsertDto.CustomPageVanityUrl,
                CustomPageContent = customPageUpsertDto.CustomPageContent,
                MenuItemID = customPageUpsertDto.MenuItemID
            };

            dbContext.CustomPages.Add(customPage);
            dbContext.SaveChanges();

            AddViewableRolesForCustomPage(dbContext, customPage, customPageUpsertDto);

            dbContext.SaveChanges();
            dbContext.Entry(customPage).Reload();

            return GetByCustomPageID(dbContext, customPage.CustomPageID);
        }

        public static void DeleteByCustomPageID(HippocampDbContext dbContext, int customPageID)
        {
            var pageToRemove = dbContext.CustomPages.SingleOrDefault(x => x.CustomPageID == customPageID);

            if (pageToRemove != null)
            {
                dbContext.CustomPageRoles.RemoveRange(
                    dbContext.CustomPageRoles.Where(x => x.CustomPageID == customPageID));
                dbContext.CustomPages.Remove(pageToRemove);
                dbContext.SaveChanges();
            }
        }

        public static bool IsDisplayNameUnique(HippocampDbContext dbContext, string customPageDisplayName, int? currentID)
        {
            return dbContext.CustomPages
                .Any(x => x.CustomPageDisplayName == customPageDisplayName &&
                          (currentID == null || (
                              currentID != null && x.CustomPageID != currentID)));
        }

        public static bool IsVanityUrlUnique(HippocampDbContext dbContext, string customPageVanityUrl, int? currentID)
        {
            return dbContext.CustomPages
                .Any(x => x.CustomPageVanityUrl == customPageVanityUrl &&
                          (currentID == null || (
                              currentID != null && x.CustomPageID != currentID)));
        }
    }
}


