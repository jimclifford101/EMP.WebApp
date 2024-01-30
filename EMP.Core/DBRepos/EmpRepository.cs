using EMP.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using EMP.Core.DisplayViewModels;
using EMP.Core.DBRepos.Interfaces;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace EMP.Core.DBRepos
{
    public class EmpRepository : IEmpRepository
    {
        private readonly IDbContextFactory<CoreDBContext> contextFactory;
        private readonly IConfiguration _myConfig;

        public EmpRepository(IDbContextFactory<CoreDBContext> contextFactory, IConfiguration myConfig)
        {
            this.contextFactory = contextFactory;
            _myConfig = myConfig;
        }

        public async Task<IEnumerable<EmpSearchViewModel>> SearchEmpAsync(string argsort = "", string argsortorder = "", EmpSearchModel argEmpSearchModel = null)
        {
            try
            {
                string myDbConxString = _myConfig.GetConnectionString("myConxStrAppSettings");
                int iRecordSetCount = 0;
                var resultList = new List<EmpSearchViewModel>();

                await using (SqlConnection DBConx = new SqlConnection(myDbConxString))
                {
                    DBConx.Open();

                    int iWhereValuesSent = 0;
                    String sqlSELECT = "SELECT Emp.guid as EmpGuid,Fname,Lname, StateName ";
                    sqlSELECT = sqlSELECT + "FROM Emp ";
                    sqlSELECT = sqlSELECT + "left join PlState on Emp.stid = PlState.guid ";

                    String sqlWHERE = "WHERE (1=1) ";
                    

                    // ****************************************************************
                    // WHERE Clause. Check values sent
                    // ****************************************************************
                    string sFname = argEmpSearchModel.Fname;
                    string sLname = argEmpSearchModel.Lname;
                    string sState = argEmpSearchModel.StateName;

                    if (sFname.Length > 0)
                    {
                        sqlWHERE = sqlWHERE + "AND (Fname LIKE '" + sFname  + "%') ";
                        iWhereValuesSent = 1;
                    }
                    if (sLname.Length > 0)
                    {
                        sqlWHERE = sqlWHERE + "AND (Lname LIKE '" + sLname + "%') ";
                        iWhereValuesSent = 1;
                    }
                    if (sState.Length > 0)
                    {
                        sqlWHERE = sqlWHERE + "AND (PlState.StateName LIKE '" + sState + "%') ";
                        iWhereValuesSent = 1;
                    }

                    // DEFAULT
                    if (iWhereValuesSent == 0)
                    {
                        // Should not Get Here!
                        sqlWHERE = "WHERE (1 = 2) ";
                        return Enumerable.Empty<EmpSearchViewModel>();
                    }


                    // ****************************************************************
                    // ORDER BY Clause. 
                    // ****************************************************************
                    String sqlORDERBY = "ORDER BY Fname ";

                    if (argsort == "LNAME")
                    {
                        sqlORDERBY = "ORDER BY Lname ";

                    }

                    if (argsort == "STATENAME")
                    {
                        sqlORDERBY = "ORDER BY StateName ";

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
                    sqlSELECT = sqlSELECT + sqlWHERE + sqlORDERBY ;

                    using (SqlCommand myCommand = new SqlCommand(sqlSELECT, DBConx))
                    {
                        using (SqlDataReader dr = myCommand.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    iRecordSetCount = iRecordSetCount + 1;

                                    EmpSearchViewModel objEmpSearchViewModel = new EmpSearchViewModel();
                                    objEmpSearchViewModel.EmpGuid = Convert.ToInt32(dr[0]);
                                    objEmpSearchViewModel.Fname = dr[1].ToString();
                                    objEmpSearchViewModel.Lname = dr[2].ToString();
                                    objEmpSearchViewModel.StateName = dr[3].ToString();

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

                return Enumerable.Empty<EmpSearchViewModel>();
            }
            finally
            {

            }


        }


        public async Task<IEnumerable<Emp>> GetEmpAllAsync(string argsort = "", string argsortorder = "")
        {
            using var db = this.contextFactory.CreateDbContext();
            if (argsort == "NAME")
            {
                if (argsortorder == "ASC")
                {
                    return await db.Emp.OrderBy(t => t.Fname).ToListAsync();
                }
                else
                {
                    return await db.Emp.OrderByDescending(t => t.Fname).ToListAsync();
                }
            }
            else
            {
                if (argsortorder == "ASC")
                {
                    return await db.Emp.OrderBy(t => t.Guid).ToListAsync();
                }
                else
                {
                    return await db.Emp.OrderByDescending(t => t.Guid).ToListAsync();
                }

            }
        }

        public async Task<IEnumerable<Emp>> GetEmpByNameAsync(string argname)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.Emp.Where(
                x => x.Fname.ToLower().IndexOf(argname.ToLower()) >= 0).ToListAsync();
        }

        public async Task<Emp> GetEmpByIdAsync(int argId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.Emp.FindAsync(argId);
            if (FoundItemByID != null) return FoundItemByID;

            return new Emp();
        }

        public async Task AddEmpAsync(Emp argEmp)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.Emp.Add(argEmp);
            await db.SaveChangesAsync();
        }

        public async Task UpdateEmpAsync(Emp argEmp)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.Emp.FindAsync(argEmp.Guid);
            if (FoundItemByID != null)
            {
                FoundItemByID.Fname = argEmp.Fname;
                FoundItemByID.Lname = argEmp.Lname;

                FoundItemByID.StId = argEmp.StId;

                // Other db columns

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteEmpAsync(Emp argEmp)
        {
            using var db = this.contextFactory.CreateDbContext();
            var FoundItemByID = await db.Emp.FindAsync(argEmp.Guid);
            if (FoundItemByID != null)
            {
                db.Remove(FoundItemByID);

                await db.SaveChangesAsync();
            }
        }
    }
}
