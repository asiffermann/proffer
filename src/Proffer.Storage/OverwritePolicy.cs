namespace Proffer.Storage
{
    /// <summary>
    /// Defines an overwrite policy when saving a file to a store.
    /// </summary>
    public enum OverwritePolicy
    {
        /// <summary>
        /// Always overwrite.
        /// </summary>
        Always = 0,

        /// <summary>
        /// Overwrite only if the file content is modified.
        /// </summary>
        IfContentModified = 1,

        /// <summary>
        /// Never overwrite.
        /// </summary>
        Never = 2,
    }
}