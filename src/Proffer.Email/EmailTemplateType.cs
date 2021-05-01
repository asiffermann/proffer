namespace Proffer.Email
{
    /// <summary>
    /// Type of template for a templated email.
    /// </summary>
    public enum EmailTemplateType
    {
        /// <summary>
        /// The templated subject.
        /// </summary>
        Subject,

        /// <summary>
        /// The templated body as HTML.
        /// </summary>
        BodyHtml,

        /// <summary>
        /// The templated body as plain-text.
        /// </summary>
        BodyText
    }
}
