namespace Proffer.Templating
{
    using System;

    /// <summary>
    /// A template reference can be executed on a specific context using an Apply method.
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        /// Applies the specified context on the template.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The templated result.</returns>
        /// <exception cref="InvalidContextException"></exception>
        string Apply(object context);

        /// <summary>
        /// Applies the specified context on the template with format provider.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// The templated result.
        /// </returns>
        /// <exception cref="InvalidContextException"></exception>
        string Apply(object context, IFormatProvider formatProvider);
    }
}
