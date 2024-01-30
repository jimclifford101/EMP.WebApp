using EMP.Core.DBRepos.Interfaces;
using EMP.Core.DisplayViewModels;
using EMP.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EMP.Core.DBRepos
{
    public class EmpHistRepository : IEmpHistRepository
    {
        private readonly IDbContextFactory<CoreDBContext> contextFactory;
        private readonly IConfiguration _myConfig;

        public EmpHistRepository(IDbContextFactory<CoreDBContext> contextFactory, IConfiguration myConfig)
        {
            this.contextFactory = contextFactory;
            _myConfig = myConfig;
        }

        public async Task<IEnumerable<EmpHistListViewModel>> GetEmpHistAllAsync(string argsort = "", string argsortorder = "")
        {
            try
            {
                string myDbConxString = _myConfig.GetConnectionString("myConxStrAppSettings");
                int iRecordSetCount = 0;
                var resultList = new List<EmpHistListViewModel>();

                await using (SqlConnection DBConx = new SqlConnection(myDbConxString))
                {
                    DBConx.Open();

                    int iWhereValuesSent = 0;

                    String sqlSELECT = "SELECT EmpHist.guid as EmpHistGuid,Emp.guid as EmpId,Fname,Lname,org.guid as OrgId,Orgname,Title,CAST(CASE WHEN CurrentEmp = 1 THEN 'YES' ELSE 'NO' END AS varchar(32)) as CurrentEmp ";
                    sqlSELECT = sqlSELECT + "FROM EmpHist ";
                    sqlSELECT = sqlSELECT + "left join Emp on EmpHist.guidemp = Emp.guid ";
                    sqlSELECT = sqlSELECT + "left join org on EmpHist.guidorg = org.guid ";


                    String sqlWHERE = "WHERE (1=1) ";


                    // ****************************************************************
                    // ORDER BY Clause. 
                    // ****************************************************************
                    String sqlORDERBY = "ORDER BY Fname ";

                    if (argsort == "LNAME")
                    {
                        sqlORDERBY = "ORDER BY Lname ";

                    }

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

                                    EmpHistListViewModel objEmpSearchViewModel = new EmpHistListViewModel();
                                    objEmpSearchViewModel.EmpHistGuid = Convert.ToInt32(dr[0]);
                                    objEmpSearchViewModel.EmpId = Convert.ToInt32(dr[1]);
                                    objEmpSearchViewModel.Fname = dr[2].ToString();
                                    objEmpSearchViewModel.Lname = dr[3].ToString();

                                    objEmpSearchViewModel.OrgId = Convert.ToInt32(dr[4]);
                                    objEmpSearchViewModel.Orgname = dr[5].ToString();

                                    objEmpSearchViewModel.Title = dr[6].ToString();
                                    objEmpSearchViewModel.CurrentEmp = dr[7].ToString();

                                    resultList.Add(objEmpSearchViewModel);

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

                return Enumerable.Empty<EmpHistListViewModel>();
            }
            finally
            {

            }
        }

        public async Task<IEnumerable<EmpHist>> GetEmpHistByNameAsync(string argname)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.EmpHist.Where(
                x => x.Title.ToLower().IndexOf(argname.ToLower()) >= 0).ToListAsync();
        }

        public async Task<EmpHist> GetEmpHistByIdAsync(int argId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.EmpHist.FindAsync(argId);
            if (FoundItemByID != null) return FoundItemByID;

            return new EmpHist();
        }

        public async Task AddEmpHistAsync(EmpHist argEmpHist)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.EmpHist.Add(argEmpHist);
            await db.SaveChangesAsync();
        }

        public async Task UpdateEmpHistAsync(EmpHist argEmpHist)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.EmpHist.FindAsync(argEmpHist.Guid);
            if (FoundItemByID != null)
            {
                FoundItemByID.GuidEmp = argEmpHist.GuidEmp;
                FoundItemByID.GuidOrg = argEmpHist.GuidOrg;
                FoundItemByID.Title = argEmpHist.Title;
                FoundItemByID.CurrentEmp = argEmpHist.CurrentEmp;

                // Other db columns

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteEmpHistAsync(EmpHist argEmpHist)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.EmpHist.FindAsync(argEmpHist.Guid);
            if (FoundItemByID != null)
            {
                db.Remove(FoundItemByID);

                await db.SaveChangesAsync();
            }
        }
    }
}
