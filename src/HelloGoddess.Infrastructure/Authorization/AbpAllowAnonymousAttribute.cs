﻿using System;

namespace HelloGoddess.Infrastructure.Authorization
{
    /// <summary>
    /// Used to allow a method to be accessed by any user.
    /// Suppress <see cref="AbpAuthorizeAttribute"/> defined in the class containing that method.
    /// </summary>
    public class AbpAllowAnonymousAttribute : Attribute, IAbpAllowAnonymousAttribute
    {

    }
}