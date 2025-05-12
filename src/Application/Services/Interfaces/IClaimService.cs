using DotNetTemplate.Application.Common;
using DotNetTemplate.Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetTemplate.Application.Services.Interfaces;

public interface IClaimService
{
    Task<Response<IEnumerable<ClaimDto>>> GetClaimsByUserIdAsync(Guid userId);
    Task<Response<ClaimDto>> CreateAsync(ClaimDto dto);
    Task<Response<bool>> DeleteAsync(Guid userId, string claimType);

}