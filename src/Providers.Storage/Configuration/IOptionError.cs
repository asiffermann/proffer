﻿namespace Providers.Storage.Configuration
{
    public interface IOptionError
    {
        string PropertyName { get; }

        string ErrorMessage { get; }
    }
}
