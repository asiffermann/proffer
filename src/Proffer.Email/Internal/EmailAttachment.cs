using Dawn;

namespace Proffer.Email.Internal
{
    /// <summary>
    /// A simpl email attachment file.
    /// </summary>
    /// <seealso cref="IEmailAttachment" />
    public class EmailAttachment : IEmailAttachment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
        /// </summary>
        public EmailAttachment() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAttachment" /> class.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="data">The file content.</param>
        /// <param name="contentType">The content-type.</param>
        public EmailAttachment(string fileName, byte[] data, string contentType)
        {
            Guard.Argument(fileName, nameof(fileName)).NotNull().NotEmpty();
            Guard.Argument(data, nameof(data)).NotNull().NotEmpty();
            Guard.Argument(contentType, nameof(contentType)).NotNull().NotEmpty().Contains("/");

            this.FileName = fileName;
            this.Data = data;

            string[] contentTypeParts = contentType.Split('/');

            this.MediaType = contentTypeParts[0];
            this.MediaSubtype = contentTypeParts[1];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="data">The file content.</param>
        /// <param name="mediaType">The media type.</param>
        /// <param name="mediaSubtype">The media subtype.</param>
        public EmailAttachment(string fileName, byte[] data, string mediaType, string mediaSubtype)
        {
            Guard.Argument(fileName, nameof(fileName)).NotNull().NotEmpty();
            Guard.Argument(data, nameof(data)).NotNull().NotEmpty();
            Guard.Argument(mediaType, nameof(mediaType)).NotNull().NotEmpty();
            Guard.Argument(mediaSubtype, nameof(mediaSubtype)).NotNull().NotEmpty();

            this.FileName = fileName;
            this.Data = data;
            this.MediaType = mediaType;
            this.MediaSubtype = mediaSubtype;
        }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file content.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the media type.
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// Gets or sets the media subtype.
        /// </summary>
        public string MediaSubtype { get; set; }

        /// <summary>
        /// Gets the content-type.
        /// </summary>
        public string ContentType => string.Join("/", this.MediaType, this.MediaSubtype);
    }
}
