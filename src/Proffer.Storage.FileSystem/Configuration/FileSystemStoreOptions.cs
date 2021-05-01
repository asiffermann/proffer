namespace Proffer.Storage.FileSystem.Configuration
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Proffer.Storage.Configuration;

    /// <summary>
    /// Options for an <see cref="FileSystemStore"/>.
    /// </summary>
    /// <seealso cref="StoreOptions" />
    public class FileSystemStoreOptions : StoreOptions
    {
        /// <summary>
        /// Gets or sets the root path.
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// Gets the absolute path.
        /// </summary>
        public string AbsolutePath
        {
            get
            {
                if (string.IsNullOrEmpty(this.RootPath))
                {
                    return this.FolderName;
                }

                if (string.IsNullOrEmpty(this.FolderName))
                {
                    return this.RootPath;
                }

                return Path.Combine(this.RootPath, this.FolderName);
            }
        }

        /// <summary>
        /// Validates the options.
        /// </summary>
        /// <param name="throwOnError">If set to <c>true</c>, throws an exception when the validation fails with any <see cref="IOptionError" />.</param>
        /// <returns>
        /// The <see cref="IOptionError" /> returned by the validation, if any.
        /// </returns>
        /// <exception cref="Exceptions.BadStoreConfiguration"></exception>
        public override IEnumerable<IOptionError> Validate(bool throwOnError = true)
        {
            IEnumerable<IOptionError> baseErrors = base.Validate(throwOnError);
            var optionErrors = new List<OptionError>();

            if (string.IsNullOrEmpty(this.AbsolutePath))
            {
                this.PushMissingPropertyError(optionErrors, nameof(this.AbsolutePath));
            }

            IEnumerable<IOptionError> finalErrors = baseErrors.Concat(optionErrors);
            if (throwOnError && finalErrors.Any())
            {
                throw new Exceptions.BadStoreConfiguration(this.Name, finalErrors);
            }

            return finalErrors;
        }
    }
}
