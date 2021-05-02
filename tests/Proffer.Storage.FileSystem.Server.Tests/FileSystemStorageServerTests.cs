namespace Proffer.Storage.FileSystem.Server.Tests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.PlatformAbstractions;
    using Xunit;
    using Xunit.Categories;

    [UnitTest]
    [Feature(nameof(Storage))]
    [Feature(nameof(FileSystem))]
    [Feature(nameof(Server))]
    [Feature(nameof(FileSystemStorageServerMiddleware))]
    public class FileSystemStorageServerTests
    {
        [Fact]
        public async Task Should_ReturnFileContent_When_GettingConfiguredStoreFile()
        {
            using IHost host = await BuildTestHost();

            HttpClient client = host.GetTestClient();

            HttpResponseMessage response = await client.GetAsync("/.well-known/storage/Public/TextFile.txt");

            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("42", responseString);
        }

        [Fact]
        public async Task Should_AccessContent_With_FilePublicUrl()
        {
            using IHost host = await BuildTestHost();

            IStorageFactory factory = host.Services.GetRequiredService<IStorageFactory>();
            IStore store = factory.GetStore("Public");

            IFileReference file = await store.GetAsync("TextFile.txt", withMetadata: true);

            HttpClient client = host.GetTestClient();

            HttpResponseMessage response = await client.GetAsync(file.PublicUrl);

            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("42", responseString);
        }

        [Fact]
        public async Task Should_ReturnStatus403_With_PrivateStore()
        {
            using IHost host = await BuildTestHost();

            IStorageFactory factory = host.Services.GetRequiredService<IStorageFactory>();
            IStore store = factory.GetStore("Private");

            IFileReference file = await store.GetAsync("TextFile.txt", withMetadata: true);

            HttpClient client = host.GetTestClient();

            HttpResponseMessage response = await client.GetAsync(file.PublicUrl);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            Assert.Throws<HttpRequestException>(() => response.EnsureSuccessStatusCode());
        }

        [Fact]
        public async Task Should_ReturnStatus404_With_UnknownFile()
        {
            using IHost host = await BuildTestHost();

            HttpClient client = host.GetTestClient();

            HttpResponseMessage response = await client.GetAsync("/.well-known/storage/Public/Unknown.txt");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Throws<HttpRequestException>(() => response.EnsureSuccessStatusCode());
        }

        private static async Task<IHost> BuildTestHost()
        {
            string basePath = PlatformServices.Default.Application.ApplicationBasePath.TrimEnd('\\').TrimEnd('/');
            IConfigurationRoot configuration = null;

            IHost host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureAppConfiguration((builderContext, configBuilder) =>
                        {
                            configBuilder
                                .SetBasePath(basePath)
                                .AddJsonFile("appsettings.json", optional: true)
                                .AddJsonFile("appsettings.development.json", optional: true)
                                .AddEnvironmentVariables();

                            configuration = configBuilder.Build();
                        })
                        .ConfigureServices((builderContext, services) =>
                        {
                            services
                                .AddStorage(configuration)
                                .AddFileSystemStorage(basePath)
                                .AddFileSystemExtendedProperties()
                                .AddFileSystemStorageServer(o => o.BaseUri = new Uri("http://localhost/"));
                        })
                        .Configure(app =>
                        {
                            app.UseFileSystemStorageServer();
                        });
                })
                .StartAsync();

            return host;
        }
    }
}
