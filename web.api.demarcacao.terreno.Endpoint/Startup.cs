using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using web.api.demarcacao.terreno.Endpoint.Config.Swagger.Security;
using web.api.demarcacao.terreno.Endpoint.Helpers.AuthHandler;
using web.api.demarcacao.terreno.Endpoint.Helpers.AuthHandler.Requirement;
using web.api.demarcacao.terreno.Endpoint.Helpers.Middleware;
using web.api.demarcacao.terreno.Endpoint.Models.Automapper;
using web.api.demarcacao.terreno.Endpoint.Models.HandleValidaiton;
using web.api.demarcacao.terreno.IoC;
using web.api.demarcacao.terreno.Service.Automapper;

namespace web.api.demarcacao.terreno.Endpoint
{
    /// <summary>
    /// Classe startup
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">ServiceCollection for register dependency injection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
            .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.Formatting = Formatting.Indented;
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(
            c =>
            {
                c.ExampleFilters();
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Demarcação Terreno API",
                        Version = "v1",
                        Description = @"API que será responsável por fazer a gestão de empreendimentos e o seus terrenos/lotes"
                    });

                c.AddSecurityDefinition("Bearer", new BearerJwtSecuritySchema());
                c.AddSecurityRequirement(new BearerJwtSecurityRequirement());

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddHealthChecks();

            services.RegisterDependencies(Configuration);

            services.AddAutoMapper(typeof(RequestToEntityProfile),
                                   typeof(ModelToRequestProfile));

            services.AddScoped<IHandleValidation, HandleValidation>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CadEmp", policy => policy.Requirements.Add(new InterfaceRequirement("CAD_EMP")));
                options.AddPolicy("AtualEmp", policy => policy.Requirements.Add(new InterfaceRequirement("ATUAL_EMP")));
                options.AddPolicy("ExcEmp", policy => policy.Requirements.Add(new InterfaceRequirement("EXC_EMP")));
                options.AddPolicy("ListEmp", policy => policy.Requirements.Add(new InterfaceRequirement("LIST_EMP")));
                options.AddPolicy("CadTerr", policy => policy.Requirements.Add(new InterfaceRequirement("CAD_TERR")));
                options.AddPolicy("AtualTerr", policy => policy.Requirements.Add(new InterfaceRequirement("ATUAL_TERR")));
                options.AddPolicy("ExcTerr", policy => policy.Requirements.Add(new InterfaceRequirement("EXC_TERR")));
                options.AddPolicy("ListTerr", policy => policy.Requirements.Add(new InterfaceRequirement("LIST_TERR")));
            });

            services.AddScoped<IAuthorizationHandler, AcaoPermissaoHandler>();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("secretJwt"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<IList<IpRule>, List<IpRule>>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                            $"Demarcação Terreno API {description.GroupName.ToUpperInvariant()}");
                }
            });

            app.UseRouting();

            app.UseMiddleware<MaxRequestPerMinutesMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/health");
        }
    }
}
