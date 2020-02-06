using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using InspectorServices.Domain.Models;

namespace InspectorServices.Domain
{
    public class InspectionService : IInspectionService
    {
        private readonly IRepository<Inspection> repository;
        
        public InspectionService(IRepository<Inspection> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(Inspection inspection)
        {
            await repository.AddAsync(inspection);
            await repository.SaveAsync();
        }

        public async Task<Inspection> FindAsync(int id) => await repository.FindAsync(id);

        public async Task<List<Inspection>> GetAllAsync() => await repository.Query.ToListAsync();

        public async Task RemoveAsync(Inspection inspection)
        {
            repository.Remove(inspection);
            await repository.SaveAsync();
        }

        public async Task UpdateAsync(Inspection inspection)
        {
            repository.Update(inspection);
            await repository.SaveAsync();
        }
    }
}
