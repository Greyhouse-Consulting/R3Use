using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NPoco;
using NPoco.FluentMappings;
using R3Use.Core.Entities;
using R3Use.Core.Repository;
using R3Use.Core.Repository.Contracts;
using R3Use.Infrastructure;
using R3Use.Web.Models;
using R3Use.Web.Repositories.Things;
using R3Use.Web.ViewModels;

namespace R3Use.Web
{
    public class Startup
    {
        private IHostingEnvironment HostingEnvironment { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            HostingEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // Add framework services.
            services.AddSingleton<IThingsRepository, ThingsRepository>();
            services.AddMvc();
        
            // Configure Mapster
            ConfigureMapster();

            // Setup IoC
            var container = new ServiceContainer(new ContainerOptions() { EnablePropertyInjection = false });
            container.Register<IAssignmentRepository, AssignmentRepository>();
            container.RegisterInstance(CreateDatabaseInstance(HostingEnvironment));

            IAdapter adapter = new Adapter();
            container.RegisterInstance(adapter);

            container.ScopeManagerProvider = new StandaloneScopeManagerProvider();

            return container.CreateServiceProvider(services);
        }

        private IDatabase CreateDatabaseInstance(IHostingEnvironment env)
        {
 
            Database database;
            if (env.IsDevelopment())
            {
                var s = new SqliteIntegration();
                s.EnsureSharedConnectionConfigured();

                database = s.CreateDatabase();

                s.RecreateSchema();
            }
            else
            {
                database = new Database("Server=localhost;Database=npoco;Trusted_Connection=True;",
                    DatabaseType.SqlServer2012,
                    SqlClientFactory.Instance);
            }

            var dbFactory = DatabaseFactory.Config(x =>
            {
                var fluentConfig = FluentMappingConfiguration.Configure(new NPocoLabMappings());
                x.UsingDatabase(() => database);
                x.WithFluentConfig(fluentConfig);
            });

            return dbFactory.GetDatabase();
        }

        private void ConfigureMapster()
        {
            TypeAdapterConfig<Assignment, AssignmentModel>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Id, src => src.Id)
                .Compile();

            TypeAdapterConfig<AssignmentModel, Assignment>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Id, src => src.Id)
                .Compile();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var angularRoutes = new[] {
                 "/home",
                 "/about"
             };

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.HasValue && null != angularRoutes.FirstOrDefault(
                    (ar) => context.Request.Path.Value.StartsWith(ar, StringComparison.OrdinalIgnoreCase)))
                {
                    context.Request.Path = new PathString("/");
                }

                await next();
            });

            app.UseCors("AllowAllOrigins");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    internal class StandaloneScopeManagerProvider : ScopeManagerProvider
    {
        protected override IScopeManager CreateScopeManager(IServiceFactory serviceFactory)
        {
            return new StandaloneScopeManager(serviceFactory);
        }
    }
    internal class StandaloneScopeManager : IScopeManager
    {
        private readonly ThreadLocal<Scope> _currentScope = new ThreadLocal<Scope>();

        public StandaloneScopeManager(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        public Scope BeginScope()
        {
            var scope = new Scope(this, null);
            _currentScope.Value = scope;
            return scope;
        }

        public void EndScope(Scope scope)
        {
            _currentScope.Value = null;
        }

        public Scope CurrentScope
        {
            get { return _currentScope.Value; }
            set { _currentScope.Value = value; }
        }

        public IServiceFactory ServiceFactory { get; }
    }
}
