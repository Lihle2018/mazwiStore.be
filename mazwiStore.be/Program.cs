
using Azure.Storage.Blobs;
using mazwiStore.be.Data;
using mazwiStore.be.Data.Interfaces;
using mazwiStore.be.Repositories;
using mazwiStore.be.Repositories.Interfaces;
using mazwiStore.be.Services;
using mazwiStore.be.Services.Interfaces;

namespace mazwiStore.be
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            builder.Services.AddSingleton<IStoreContext>(sp =>
            {
                var connectionString = builder.Configuration["DatabaseSettings:ConnectionString"];
                var databaseName = builder.Configuration["DatabaseSettings:DatabaseName"];
                var phonesCollectionName = builder.Configuration["DatabaseSettings:CollectionName"];
                return new StoreContext(connectionString, databaseName, phonesCollectionName);
            });

            builder.Services.AddScoped<IStorageService>(sp =>
            {
                var blobServiceClient = new BlobServiceClient(builder.Configuration["AzureBlobStorage:ConnectionString"]);
                var storageContainerName = builder.Configuration["AzureBlobStorage:ContainerName"];
                return new StorageService(blobServiceClient, storageContainerName);
            });

            builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
