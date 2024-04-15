using NXWalks.API.Models.Domains;
using System.Runtime.CompilerServices;

namespace NXWalks.API.Repositories
{
    public interface IWalksRepositories
    {
    Task<Walks> CreateAsync(Walks walks);

    Task<List<Walks>> GetAllWalksAsync(string? filterOn = null , string? filterQuery = null,
        string? sortBy = null , bool isAssending = true,
        int pageNumber = 1 , int pageSize = 100);

    Task<Walks?> GetWalkByID(Guid id);

    Task<Walks?> UpdateWalkAsync(Guid id , Walks walks);

    Task<Walks?> DeleteWalk(Guid id);

    }
}
