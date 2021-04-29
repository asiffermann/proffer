﻿namespace Providers.Storage
{
    using System;
    using System.Collections.Generic;

    public interface IFileProperties
    {
        DateTimeOffset? LastModified { get; }

        long Length { get; }

        string ContentType { get; set; }

        string ETag { get; }

        string CacheControl { get; set; }

        string ContentMD5 { get; }

        IDictionary<string, string> Metadata { get; }
    }
}
