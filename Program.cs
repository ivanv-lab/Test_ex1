using Test_ex.DTO;
using Test_ex.Maps;
using Test_ex.Models;
using Test_ex.Repositories.Impl;
using Test_ex.Repositories.Interfaces;
using Test_ex.Services.Impl;
using Test_ex.Services.Interfaces;
using Test_ex.Utils;

namespace Test_ex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();

            builder.Services.AddScoped<AppDbContext>();

            builder.Services.AddTransient<IMapper<Specialization, SpecializationDto>, SpecializationMap>();
            builder.Services.AddTransient<IMapper<Region,RegionDto>,RegionMap>();
            builder.Services.AddTransient<IMapper<Cabinet,CabinetDto>,CabinetMap>();
            builder.Services.AddTransient<IMapper<Patient, PatientDto>, PatientMap>();
            builder.Services.AddTransient<IMapper<Doctor,DoctorDto>,DoctorMap>();

            builder.Services.AddTransient<ISpecializationService, SpecializationService>();
            builder.Services.AddTransient<IRegionService, RegionService>();
            builder.Services.AddTransient<ICabinetService, CabinetService>();
            builder.Services.AddTransient<IPatientService, PatientService>();
            builder.Services.AddTransient<IDoctorService, DoctorService>();

            builder.Services.AddTransient<ISpecializationRepo, SpecializationRepo>();
            builder.Services.AddTransient<IRegionRepo, RegionRepo>();
            builder.Services.AddTransient<ICabinetRepo, CabinetRepo>();
            builder.Services.AddTransient<IPatientRepo, PatientRepo>();
            builder.Services.AddTransient<IDoctorRepo, DoctorRepo>();


            var app = builder.Build();

            using var scope=app.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //dbContext.Database.EnsureCreatedAsync();
            DbInitialize.Initialize(dbContext);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
