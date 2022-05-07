using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using UniversityBusinessLogic.BusinessLogic;
using UniversityBusinessLogic.OfficePackage;
using UniversityBusinessLogic.OfficePackage.Implements;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.StoragesContracts;
using UniversityDatabaseImplement.Implements;

namespace UniversityRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MailLogic.MailConfig(new MailConfigBindingModel
            {
                SmtpClientHost = configuration["SmtpClientHost"],
                SmtpClientPort = Convert.ToInt32(configuration["SmtpClientPort"]),
                MailLogin = configuration["MailLogin"],
                MailPassword = configuration["MailPassword"],
            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICostItemStorage, CostItemStorage>();
            services.AddTransient<IDisciplineStorage, DisciplineStorage>();
            services.AddTransient<IEducationStorage, EducationStorage>();
            services.AddTransient<IPaymentStorage, PaymentStorage>();
            services.AddTransient<IUserStorage, UserStorage>();

            services.AddTransient<ICostItemLogic, CostItemLogic>();
            services.AddTransient<IDisciplineLogic, DisciplineLogic>();
            services.AddTransient<IEducationLogic, EducationLogic>();
            services.AddTransient<IGraphicLogic, GraphicLogic>();
            services.AddTransient<IReportLogic, ReportLogic>();
            services.AddTransient<MailLogic>();
            services.AddTransient<IPaymentLogic, PaymentLogic>();
            services.AddTransient<IUserLogic, UserLogic>();

            services.AddTransient<AbstractSaveToExcel, SaveToExcel>();
            services.AddTransient<AbstractSaveToWord, SaveToWord>();
            services.AddTransient<AbstractSaveToPdf, SaveToPdf>();

            services.AddScoped<DatabaseHelper>();
            DatabaseHelper database = services.BuildServiceProvider().GetRequiredService<DatabaseHelper>();
            database.Load();

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UniversityRestApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniversityRestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
