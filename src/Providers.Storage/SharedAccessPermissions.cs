namespace Providers.Storage
{
    using System;

    /// <summary>
    /// Specifies the set of possible permissions for a shared access policy.
    /// </summary>
    [Flags]
    public enum SharedAccessPermissions
    {
        /// <summary>
        /// No shared access granted.
        /// </summary>
        None = 0,

        /// <summary>
        /// Read access granted.
        /// </summary>
        Read = 1,

        /// <summary>
        /// Write access granted.
        /// </summary>
        Write = 2,

        /// <summary>
        /// Delete access granted.
        /// </summary>
        Delete = 4,

        /// <summary>
        /// List access granted.
        /// </summary>
        List = 8,

        /// <summary>
        /// Add access granted.
        /// </summary>
        Add = 16,

        /// <summary>
        /// Create access granted.
        /// </summary>
        Create = 32
    }
}
