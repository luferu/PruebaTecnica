using Microsoft.EntityFrameworkCore;
using PruebaTecnica.DAL.DbContext;
using PruebaTecnica.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.DAL.Repositories
{
    public class ApiLogRepository : Repository<ApiLog>, IApiLogRepository
    {
        private readonly AppDbContext _context;

        public ApiLogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(ApiLog entity)
        {
            
            entity.Timestamp = DateTime.UtcNow; 

            await base.AddAsync(entity); 
        }
    }
}
