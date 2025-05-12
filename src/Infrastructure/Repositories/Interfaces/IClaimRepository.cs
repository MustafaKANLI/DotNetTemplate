using DotNetTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetTemplate.Infrastructure.Repositories.Interfaces;

public interface IClaimRepository: IGenericRepository<Claim>
{
    Task<IEnumerable<Claim>> GetClaimsByUserIdAsync(Guid userId);
}