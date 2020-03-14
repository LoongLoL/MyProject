using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Authorization;
using SwaggerAndJwt.Controllers;
using SwaggerAndJwt.Helper;

namespace SwaggerAndJwt
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }
        public string ApiName { get; set; } = "SwaggerAndJwt";

        // This method gets called by the runtime. Use this method to add services to the container.
        private static readonly string apiName = "swagger_api";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new Appsettings(Env.ContentRootPath));

            //services.AddControllers();
            services.AddControllers(c => { c.Filters.Add(new AuthorizeFilter()); });//��һ�����ñ���ȫ����controller����Ҫ������֤������ʹ��AllowAnonymous���Ա��Ϊ������֤
            #region IdentityServer����
            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApis())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                //.AddTestUsers(IdentityServerConfig.GetUsers());
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            builder.AddDeveloperSigningCredential();
            #endregion

            #region swagger����
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    // {ApiName} �����ȫ�ֱ����������޸�
                    Version = "V1",
                    Title = $"{ApiName} �ӿ��ĵ�����Netcore 3.1",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "�����ַ@Email", Url = new Uri("http://���ӵ�ַ.com") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("http://���ӵ�ַ.com") }
                });
                c.OrderActionsBy(o => o.RelativePath);

                //����������ʾsummaryע�ͣ���Ҫ�����ԡ�������ҳ����������xml�ļ���ͬʱ���������ԡ�������ҳ�����ú���1591���档
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);//Ĭ�ϵĵڶ���������false������Ϊtrue�ſ�����ʾcontroller��ע�ͣ��ǵ��޸�

                //var xmlModelPath = Path.Combine(basePath, "�������.xml");//����������������swagger�����Ϣ�ĵ�
                //c.IncludeXmlComments(xmlModelPath);

                //swagger����jwt��֤
                //����Ȩ��С��
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //��header�����token�����ݵ���̨
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���)ֱ���������������Bearer {token}(ע������֮����һ���ո�) \"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });

            });
            #endregion

            #region jwt����

            //��ȡ�����ļ�
            //var symmetricKeyAsBase64 = AppSecretConfig.Audience_Secret_String;
            //var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            //var signingKey = new SymmetricSecurityKey(keyByteArray);
            //var Issuer = Appsettings.app(new string[] { "Authentication", "JwtBearer", "Issuer" });
            //var Audience = Appsettings.app(new string[] { "Authentication", "JwtBearer", "Audience" });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.SaveToken = true;
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        //�Ƿ�����Կ��֤��keyֵ
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = signingKey,

            //        //�Ƿ�����������֤�ͷ�����
            //        ValidateIssuer = true,
            //        ValidIssuer = Issuer,

            //        //�Ƿ�����������֤�Ͷ�����
            //        ValidateAudience = true,
            //        ValidAudience = Audience,

            //        //��֤ʱ���ƫ����
            //        ClockSkew = TimeSpan.FromSeconds(30),//TimeSpan.Zero,
            //        //�Ƿ���ʱ����֤
            //        ValidateLifetime = true,

            //        //�Ƿ�����Ʊ�����й���ʱ��
            //        RequireExpirationTime = true
            //    };

            //    options.Events = new JwtBearerEvents
            //    {
            //        OnAuthenticationFailed = context =>
            //        {
            //            // ������ڣ����<�Ƿ����>��ӵ�������ͷ��Ϣ��
            //            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            //            {
            //                context.Response.Headers.Add("Token-Expired", "true");
            //            }
            //            return Task.CompletedTask;
            //        }
            //    };
            //});


            // 1����Ȩ���������[Authorize(Roles = "Admin")]������ͬ�����ô����ǲ�����controller�У�д��� roles ��
            // Ȼ����ôд [Authorize(Policy = "Admin")]
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());//������ɫ
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            //    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));//��Ĺ�ϵ
            //    options.AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));//�ҵĹ�ϵ
            //});
            #endregion


            services.AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:3163";    //����Identityserver����Ȩ��ַ
                    options.RequireHttpsMetadata = false;           //����Ҫhttps    
                    options.ApiName = apiName;  //api��name����Ҫ��config��������ͬ
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");

                    //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseIdentityServer();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
