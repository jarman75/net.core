using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using IdentityManagment.Data;

namespace IdentityManagment.Controllers
{
    public partial class Exportuser_dbController : ExportController
    {
        private readonly user_dbContext context;
        private readonly user_dbService service;

        public Exportuser_dbController(user_dbContext context, user_dbService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/user_db/users/csv")]
        [HttpGet("/export/user_db/users/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsers(), Request.Query), fileName);
        }

        [HttpGet("/export/user_db/users/excel")]
        [HttpGet("/export/user_db/users/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsers(), Request.Query), fileName);
        }
    }
}
