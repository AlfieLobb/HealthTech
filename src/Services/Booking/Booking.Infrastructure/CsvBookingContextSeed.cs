//using Booking.Infrastructure;
//using CsvHelper;
//using CsvHelper.Configuration;
//using Microsoft.AspNetCore.Hosting;
//using System.Globalization;
//using System.IO.Abstractions;


//namespace Inventory.API.Infrastructure.ContextSeed.Sources;

//System.IO.Abstractions for the IFileSystem
//public class CsvBookingContextSeed(IWebHostEnvironment env, IFileSystem fileSystem) : IDbSeeder<BookingContext>
//{
//    public async Task SeedAsync(BookingContext context)
//    {
//        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
//        {
//            HasHeaderRecord = false
//        };
//        if (!context.Patients.Any())
//        {
//            await SeedEntityAsync<Patient, PatientClassMap>(context, configuration);
//        }
//        if (!context.Approver.Any())
//        {
//            await SeedEntityAsync<Approver, ApproverClassMap>(context, configuration);
//        }
//        if (!context.Appointments.Any())
//        {
//            await SeedEntityAsync<Appointment, AppointmentsClassMap>(context, configuration);
//        }
//    }

//    private async Task SeedEntityAsync<TEntity, TClassMap>(BookingContext context, CsvConfiguration configuration)
//        where TEntity : class
//        where TClassMap : ClassMap<TEntity>
//    {
//        var path = PathFor(typeof(TEntity).Name);
//        var reader = fileSystem.File.Exists(path) ? fileSystem.File.OpenText(path) : throw new FileNotFoundException($"File not found {Path.GetFullPath(path)}", path);
//        using var csv = new CsvReader(reader, configuration);

//        csv.Context.RegisterClassMap<TClassMap>();
//        var records = csv.GetRecords<TEntity>();

//        await context.Set<TEntity>().AddRangeAsync(records);
//        await context.SaveChangesAsync();
//    }
//    private string PathFor(string type) => Path.Combine(env.ContentRootPath, "Setup", "csv", $"{type}s.csv");
//}

//file class AppointmentClassMap : ClassMap<Appointment>
//{
//    public AppointmentsClassMap()
//    {
//        Map(p => p.Id).Ignore();
//        Map(p => p.Name).Index(0);
//    }
//}