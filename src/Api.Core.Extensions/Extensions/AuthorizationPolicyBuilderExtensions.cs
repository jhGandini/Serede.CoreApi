﻿using Microsoft.AspNetCore.Authorization;

namespace Api.Core.Extensions.Extensions;

public static class AuthorizationPolicyBuilderExtensions
{
    public static AuthorizationPolicyBuilder RequireScope(this AuthorizationPolicyBuilder builder, params string[] scope)
    {
        return builder.RequireClaim("scope", scope);
    }
}