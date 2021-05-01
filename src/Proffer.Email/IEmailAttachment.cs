namespace Proffer.Email
{
    /// <summary>
    /// An email attachment file.
    /// </summary>
    public interface IEmailAttachment
    {
        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file content.
        /// </summary>
        byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the media type.
        /// </summary>
        string MediaType { get; set; }

        /// <summary>
        /// Gets or sets the media subtype.
        /// </summary>
        string MediaSubtype { get; set; }

        /// <summary>
        /// Gets the content-type.
        /// </summary>
        string ContentType { get; }
    }
}
