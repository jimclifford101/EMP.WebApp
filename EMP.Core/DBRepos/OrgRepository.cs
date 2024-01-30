using EMP.Core.DBRepos.Interfaces;
using EMP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Core.DBRepos
{
    public class OrgRepository : IOrgRepository
    {
        private readonly IDbContextFactory<CoreDBContext> contextFactory;

        public OrgRepository(IDbContextFactory<CoreDBContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Org>> GetOrgAllAsync(string argsort = "", string argsortorder = "")
        {
            using var db = this.contextFactory.CreateDbContext();
            if (argsort == "NAME")
            {
                if (argsortorder == "ASC")
                {
                    return await db.Org.OrderBy(t => t.Orgname).ToListAsync();
                }
                else
                {
                    return await db.Org.OrderByDescending(t => t.Orgname).ToListAsync();
                }
            }
            else
            {
                if (argsortorder == "ASC")
                {
                    return await db.Org.OrderBy(t => t.Guid).ToListAsync();
                }
                else
                {
                    return await db.Org.OrderByDescending(t => t.Guid).ToListAsync();
                }

            }
        }

        public async Task<IEnumerable<Org>> GetOrgByNameAsync(string argname)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.Org.Where(
                x => x.Orgname.ToLower().IndexOf(argname.ToLower()) >= 0).ToListAsync();
        }

        public async Task<Org> GetOrgByIdAsync(int argId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.Org.FindAsync(argId);
            if (FoundItemByID != null) return FoundItemByID;

            return new Org();
        }

        public async Task AddOrgAsync(Org argOrg)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.Org.Add(argOrg);
            await db.SaveChangesAsync();
        }

        public async Task UpdateOrgAsync(Org argOrg)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.Org.FindAsync(argOrg.Guid);
            if (FoundItemByID != null)
            {
                FoundItemByID.Orgname = argOrg.Orgname;

                // Other db columns

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteOrgAsync(Org argOrg)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.Org.FindAsync(argOrg.Guid);
            if (FoundItemByID != null)
            {
                db.Remove(FoundItemByID);

                await db.SaveChangesAsync();
            }
        }
    }
}
