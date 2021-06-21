using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation GetById(string id);
        Compensation Add(Compensation compenstation);
        Task SaveAsync();
    }
}