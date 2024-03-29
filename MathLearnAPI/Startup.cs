﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData.Batch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using MathLearnAPI.Models;
using Microsoft.AspNet.OData.Builder;
using System.IO;

namespace MathLearnAPI
{
    public class Startup
    {
        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        internal static String UploadFolder { get; private set; }
        internal static String ConnectionString { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            UploadFolder = Path.Combine(env.ContentRootPath, @"uploads");
            if (!Directory.Exists(UploadFolder))
            {
                Directory.CreateDirectory(UploadFolder);
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:20020",
                                        "https://localhost:20020")
                                .AllowAnyHeader()
                                .AllowAnyMethod(); ;
                });
            });

            if (String.IsNullOrEmpty(ConnectionString))
                ConnectionString = Configuration.GetConnectionString("DebugConnection");

            services.AddDbContext<acquizdbContext>(opt => opt.UseSqlServer(ConnectionString).UseLoggerFactory(MyLoggerFactory));
            services.AddOData();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Knowledge>("Knowledges");
            modelBuilder.EntitySet<Questionbank>("Questionbanks");
            modelBuilder.EntitySet<Qbklink>("Qbklinks");
            modelBuilder.EntitySet<Permuser>("Permusers");
            modelBuilder.EntitySet<Quiz>("Quizs");
            modelBuilder.EntitySet<Quizsection>("Quizsections");
            modelBuilder.EntitySet<Quizfaillog>("Quizfaillogs");
            modelBuilder.EntitySet<Quizuser>("Quizusers");
            modelBuilder.EntitySet<Tag>("Tags");
            modelBuilder.EntitySet<Useraward>("Userawards");

            //var createProduct = modelBuilder.EntityType<ProductFamily>().Action("CreateProduct");
            //createProduct.Parameter<string>("Name");
            //createProduct.Returns<int>();
            modelBuilder.Namespace = typeof(Knowledge).Namespace;

            var model = modelBuilder.GetEdmModel();
            app.UseODataBatching();
            app.UseMvc(builder =>
            {
                builder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();

                builder.MapODataServiceRoute("odata", "odata", model, new DefaultODataBatchHandler());
            });
        }
    }
}
