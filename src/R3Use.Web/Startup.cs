﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NPoco;
using NPoco.Core;
using NPoco.Core.Repository;
using NPoco.Core.Repository.Contracts;
using NPoco.FluentMappings;
using Serilog;

namespace NPocoLab.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.ColoredConsole()
                .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var container = new ServiceContainer();
            container.Register<IProspectRepository, ProspectRepository>();



            // Add framework services.
            services.AddMvc();

            ConfigureMapster();

            IAdapter adapter = new Adapter();

            container.RegisterInstance(CreateDatabaseInstance());

            
            container.RegisterInstance(adapter);

            container.ScopeManagerProvider = new StandaloneScopeManagerProvider();



            return container.CreateServiceProvider(services);
            
        }

        private IDatabase CreateDatabaseInstance()
        {
            var fluentConfig = FluentMappingConfiguration.Configure(new NPocoLabMappings());

            var dbFactory = DatabaseFactory.Config(x =>
            {

                //x.UsingDatabase(() => new Database(new SqlConnection("Server=localhost;Database=npoco;Trusted_Connection=True;")));
                x.UsingDatabase(() => new Database("Server=localhost;Database=npoco;Trusted_Connection=True;", DatabaseType.SqlServer2012, SqlClientFactory.Instance));
                x.WithFluentConfig(fluentConfig);
            });

            return dbFactory.GetDatabase();
        }

        private void ConfigureMapster()
        {
            TypeAdapterConfig<Prospect, ProspectViewModel>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Id, src => src.Id)
                .Compile();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

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