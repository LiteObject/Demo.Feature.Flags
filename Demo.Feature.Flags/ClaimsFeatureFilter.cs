﻿using Microsoft.FeatureManagement;

namespace Demo.Feature.Flags
{
    [FilterAlias("Claims")] // How we will refer to the filter in configuration
    public class ClaimsFeatureFilter : IFeatureFilter
    {
        // Used to access HttpContext
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimsFeatureFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Evaluate(FeatureFilterEvaluationContext context)
        {
            // Get the ClaimsFilterSettings from configuration
            var settings = context.Parameters.Get<ClaimsFilterSettings>();

            // Retrieve the current user (ClaimsPrincipal)
            System.Security.Claims.ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;

            // Only enable the feature if the user has ALL the required claims
            bool isEnabled = settings.RequiredClaims
                .All(claimType => user?.HasClaim(claim => claim.Type == claimType));

            return isEnabled;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
