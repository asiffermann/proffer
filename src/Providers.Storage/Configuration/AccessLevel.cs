namespace Providers.Storage.Configuration
{
    /// <summary>
    /// Defines a store access level.
    /// </summary>
    public enum AccessLevel
    {
        /// <summary>
        /// No public access.
        /// </summary>
        Private = 0,

        /// <summary>
        /// Public files without listing.
        /// </summary>
        Confidential = 1,

        /// <summary>
        /// Public access.
        /// </summary>
        Public = 2,
    }
}
