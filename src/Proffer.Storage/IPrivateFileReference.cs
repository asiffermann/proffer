namespace Proffer.Storage
{
    /// <summary>
    /// A reference of a stored file at a given path.
    /// </summary>
    public interface IPrivateFileReference
    {
        /// <summary>
        /// Gets the file path.
        /// </summary>
        string Path { get; }
    }
}
