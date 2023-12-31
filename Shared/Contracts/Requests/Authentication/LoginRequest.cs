﻿using Shared.Validation.Abstraction;

namespace Shared.Contracts.Requests.Authentication
{
    public sealed record LoginRequest(string Email,
                                      string Password) : IValidatable;
}
