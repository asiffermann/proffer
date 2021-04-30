namespace Providers.Storage
{
    using System;

    /// <summary>
    /// Represents a shared access policy, which specifies the start time, expiry time, and permissions for a shared access signature.
    /// </summary>
    public interface ISharedAccessPolicy
    {
        /// <summary>
        /// Gets the start time.
        /// </summary>
        DateTimeOffset? StartTime { get; }

        /// <summary>
        /// Gets the expiry time.
        /// </summary>
        DateTimeOffset? ExpiryTime { get; }

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        SharedAccessPermissions Permissions { get; }
    }
}
