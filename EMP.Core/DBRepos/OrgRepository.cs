using EMP.Core.DBRepos.Interfaces;
using EMP.Core.DisplayViewModels;
using EMP.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _myConfig;

        public OrgRepository(IDbContextFactory<CoreDBContext> contextFactory, IConfiguration myConfig)
        {
            this.contextFactory = contextFactory;
            _myConfig = myConfig;
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


        public async Task<IEnumerable<OrgSearchViewModel>> SearchOrgAsync(string argsort = "", string argsortorder = "", OrgSearchInputModel argOrgSearchInputModel = null)
        {
            try
            {
                string myDbConxString = _myConfig.GetConnectionString("myConxStrAppSettings");
                int iRecordSetCount = 0;
                var resultList = new List<OrgSearchViewModel>();

                await using (SqlConnection DBConx = new SqlConnection(myDbConxString))
                {
                    DBConx.Open();

                    String sqlSELECT = "SELECT TOP 100 Guid AS [Guid],Orgname ";
                    sqlSELECT = sqlSELECT + "FROM Org ";
                    //sqlSELECT = sqlSELECT + "left join OtherTable on Org.Guid = OtherTable.Guid ";

                    String sqlWHERE = "WHERE (1=1) ";


                    // ****************************************************************
                    // WHERE Clause. Check values sent
                    // ****************************************************************
                    string sOrgname = argOrgSearchInputModel.Orgname;

                    if (sOrgname.Length > 0)
                    {
                        sqlWHERE = sqlWHERE + "AND (Orgname LIKE '" + sOrgname + "%') ";
                    }


                    // ****************************************************************
                    // ORDER BY Clause. 
                    // ****************************************************************
                    String sqlORDERBY = "ORDER BY Orgname ";   //DEFAULT SORT

                    //if (argsort == " $ otherColumnName $ ")
                    //{
                    //    sqlORDERBY = "ORDER BY $ otherColumnName $ ";
                    //}

                    if (argsortorder == "ASC")
                    {
                        sqlORDERBY = sqlORDERBY + "ASC";
                    }
                    else
                    {
                        sqlORDERBY = sqlORDERBY + "DESC";
                    }


                    // ****************************************************************
                    // Build Final SELECT with WHERE & Order BY
                    // ****************************************************************
                    sqlSELECT = sqlSELECT + sqlWHERE + sqlORDERBY;

                    using (SqlCommand myCommand = new SqlCommand(sqlSELECT, DBConx))
                    {
                        using (SqlDataReader dr = myCommand.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    iRecordSetCount = iRecordSetCount + 1;

                                    OrgSearchViewModel objOrgSearchViewModel = new OrgSearchViewModel();

                                    objOrgSearchViewModel.Guid = Convert.ToInt32(dr[0]);
                                    objOrgSearchViewModel.Orgname = dr[1].ToString();

                                    resultList.Add(objOrgSearchViewModel);
                                }
                            }
                        }
                    }
                    DBConx.Close();
                }

                return resultList;

            }
            catch (Exception ex)
            {
                return Enumerable.Empty<OrgSearchViewModel>();
            }
            finally
            {

            }

        }
    }
}
