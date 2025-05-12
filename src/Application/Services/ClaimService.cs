using DotNetTemplate.Application.Services.Interfaces;
using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using DotNetTemplate.Application.Common;
using DotNetTemplate.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetTemplate.Application.Services;

public class ClaimService : IClaimService
{
    private readonly IClaimRepository _claimRepository;

    public ClaimService(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task<Response<IEnumerable<ClaimDto>>> GetClaimsByUserIdAsync(Guid userId)
    {
        var claims = await _claimRepository.GetClaimsByUserIdAsync(userId);
        if (claims == null || !claims.Any()) return new Response<IEnumerable<ClaimDto>>("No claims found for this user.");
        return new Response<IEnumerable<ClaimDto>>(claims.Adapt<List<ClaimDto>>());
    }

    public async Task<Response<ClaimDto>> CreateAsync(ClaimDto dto)
    {
        var claim = dto.Adapt<Claim>();
        await _claimRepository.AddAsync(claim);
        return new Response<ClaimDto>(claim.Adapt<ClaimDto>(), "Succeeded.");
    }

    public async Task<Response<bool>> DeleteAsync(Guid userId, string claimType)
    {
        var claims = await _claimRepository.GetClaimsByUserIdAsync(userId);
        var claimToDelete = claims.FirstOrDefault(c => c.Type == claimType);
        if (claimToDelete == null) return new Response<bool>(false, "Claim not found.");

        await _claimRepository.DeleteAsync(claimToDelete);
        return new Response<bool>(true, "Claim deleted successfully.");
    }
}