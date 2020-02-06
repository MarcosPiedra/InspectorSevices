using InspectorServices.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InspectorServices.Domain
{
    public interface IInspectionService
    {
        Task UpdateAsync(Inspection inspection);
        Task AddAsync(Inspection inspection);
        Task RemoveAsync(Inspection inspection);
        Task<Inspection> FindAsync(int id);
        Task<List<Inspection>> GetAllAsync();
    }
}