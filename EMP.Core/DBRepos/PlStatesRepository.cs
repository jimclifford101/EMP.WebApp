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
    public class PlStateRepository : IPlStateRepository
    {
        private readonly IDbContextFactory<CoreDBContext> contextFactory;

        public PlStateRepository(IDbContextFactory<CoreDBContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IEnumerable<PlState>> GetPlStateAllAsync(string argsort = "", string argsortorder = "")
        {
            using var db = this.contextFactory.CreateDbContext();
            if (argsort == "NAME")
            {
                if (argsortorder == "ASC")
                {
                    return await db.PlState.OrderBy(t => t.StateName).ToListAsync();
                }
                else
                {
                    return await db.PlState.OrderByDescending(t => t.StateName).ToListAsync();
                }
            }
            else
            {
                if (argsortorder == "ASC")
                {
                    return await db.PlState.OrderBy(t => t.Guid).ToListAsync();
                }
                else
                {
                    return await db.PlState.OrderByDescending(t => t.Guid).ToListAsync();
                }

            }
        }

        public async Task<IEnumerable<PlState>> GetPlStateByNameAsync(string argname)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.PlState.Where(
                x => x.StateName.ToLower().IndexOf(argname.ToLower()) >= 0).ToListAsync();
        }

        public async Task<PlState> GetPlStateByIdAsync(int argId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.PlState.FindAsync(argId);
            if (FoundItemByID != null) return FoundItemByID;

            return new PlState();
        }

        public async Task AddPlStateAsync(PlState argPlState)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.PlState.Add(argPlState);
            await db.SaveChangesAsync();
        }

        public async Task UpdatePlStateAsync(PlState argPlState)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.PlState.FindAsync(argPlState.Guid);
            if (FoundItemByID != null)
            {
                FoundItemByID.StateName = argPlState.StateName;

                // Other db columns

                await db.SaveChangesAsync();
            }
        }

        public async Task DeletePlStateAsync(PlState argPlState)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.PlState.FindAsync(argPlState.Guid);
            if (FoundItemByID != null)
            {
                db.Remove(FoundItemByID);

                await db.SaveChangesAsync();
            }
        }
    }
}