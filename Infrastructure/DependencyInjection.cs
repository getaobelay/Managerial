using Domain;
using Domain.Entites.Identity;
using IdentityServer4.AccessTokenValidation;
using Infrastructure.Authorization;
using Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetSection("UseInMemoryDatabase").Value.Equals("true"))
            {
 
                services.AddDbContext<ManagerialDbContext>(options =>
                            options.UseInMemoryDatabase("Manageril_InMemoryDatabase"));
            }

            else
            {
                services.AddDbContext<ManagerialDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                ));
            }

            // Enable logging of potential PII messages (Personally Identifiable Information)
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

            // add identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ManagerialDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity options and password complexity here
            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;

                //    //// Password settings
                //    //options.Password.RequireDigit = true;
                //    //options.Password.RequiredLength = 8;
                //    //options.Password.RequireNonAlphanumeric = false;
                //    //options.Password.RequireUppercase = true;
                //    //options.Password.RequireLowercase = false;

                //    //// Lockout settings
                //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //    //options.Lockout.MaxFailedAccessAttempts = 10;
            });

            // Adds IdentityServer.
            services.AddIdentityServer()
                // The AddDeveloperSigningCredential extension creates temporary key material for signing tokens.
                // This might be useful to get started, but needs to be replaced by some persistent key material for production scenarios.
                // See http://docs.identityserver.io/en/release/topics/crypto.html#refcrypto for more information.
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                // To configure IdentityServer to use EntityFramework (EF) as the storage mechanism for configuration data (rather than using the in-memory implementations),
                // see https://identityserver4.readthedocs.io/en/release/quickstarts/8_entity_framework.html
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>();



            var applicationUrl = configuration["ApplicationUrl"].TrimEnd('/');

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = applicationUrl;
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.RequireHttpsMetadata = false; // Note: Set to true in production
                    options.ApiName = IdentityServerConfig.ApiName;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Infrastructure.Authorization.Policies.ViewAllUsersPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewUsers));
                options.AddPolicy(Infrastructure.Authorization.Policies.ManageAllUsersPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageUsers));

                options.AddPolicy(Infrastructure.Authorization.Policies.ViewAllRolesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewRoles));
                options.AddPolicy(Infrastructure.Authorization.Policies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new ViewRoleAuthorizationRequirement()));
                options.AddPolicy(Infrastructure.Authorization.Policies.ManageAllRolesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageRoles));

                options.AddPolicy(Infrastructure.Authorization.Policies.AssignAllowedRolesPolicy, policy => policy.Requirements.Add(new AssignRolesAuthorizationRequirement()));
            });

            return services;
        }
    }
}