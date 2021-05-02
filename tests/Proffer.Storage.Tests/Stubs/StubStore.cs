namespace Proffer.Storage.Tests.Stubs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Proffer.Storage.Tests.Stubs.Configuration;

    public class StubStore : IStore
    {
        private readonly StubStoreOptions storeOptions;

        public StubStore(StubStoreOptions storeOptions)
        {
            this.storeOptions = storeOptions;
        }

        public string Name => this.storeOptions.Name;

        public Task InitAsync(CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public ValueTask<IFileReference[]> ListAsync(string path, bool recursive, bool withMetadata)
            => ValueTask.FromResult(Enumerable.Empty<IFileReference>().ToArray());

        public ValueTask<IFileReference[]> ListAsync(string path, string searchPattern, bool recursive, bool withMetadata)
            => ValueTask.FromResult(Enumerable.Empty<IFileReference>().ToArray());

        public ValueTask<IFileReference> GetAsync(IPrivateFileReference file, bool withMetadata)
            => ValueTask.FromResult((IFileReference)null);

        public ValueTask<IFileReference> GetAsync(Uri uri, bool withMetadata)
            => ValueTask.FromResult((IFileReference)null);

        public Task DeleteAsync(IPrivateFileReference file)
            => Task.CompletedTask;

        public ValueTask<Stream> ReadAsync(IPrivateFileReference file)
            => ValueTask.FromResult((Stream)null);

        public ValueTask<byte[]> ReadAllBytesAsync(IPrivateFileReference file)
            => ValueTask.FromResult((byte[])null);

        public ValueTask<string> ReadAllTextAsync(IPrivateFileReference file)
            => ValueTask.FromResult((string)null);

        public ValueTask<IFileReference> SaveAsync(byte[] data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string, string> metadata = null)
            => ValueTask.FromResult((IFileReference)null);

        public ValueTask<IFileReference> SaveAsync(Stream data, IPrivateFileReference file, string contentType, OverwritePolicy overwritePolicy = OverwritePolicy.Always, IDictionary<string, string> metadata = null)
            => ValueTask.FromResult((IFileReference)null);

        public ValueTask<string> GetSharedAccessSignatureAsync(ISharedAccessPolicy policy)
            => ValueTask.FromResult((string)null);
    }
}
