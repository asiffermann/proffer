namespace Providers.Storage
{
    using System;

    /// <summary>
    /// Represents a simple shared access policy, which specifies the start time, expiry time, and permissions for a shared access signature.
    /// </summary>
    /// <seealso cref="ISharedAccessPolicy" />
    public class SharedAccessPolicy : ISharedAccessPolicy
    {
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public DateTimeOffset? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the expiry time.
        /// </summary>
        public DateTimeOffset? ExpiryTime { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        public SharedAccessPermissions Permissions { get; set; }
    }
}
