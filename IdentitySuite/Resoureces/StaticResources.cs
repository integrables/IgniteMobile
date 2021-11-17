using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentitySuite.Resoureces
{
    internal class StaticResources
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
            {
                 new ApiScope("subscribers.read", "Retrive the list of subscribers"),
                 new ApiScope("reports.generate", "Generate Reports"),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                 new ApiResource
                 {
                     Name = "subscribtions",
                     DisplayName = "Subscribtions",
                     Description = "Access the subscriptions information",
                     Scopes = new List<string> {"subscribers.read"},
                     ApiSecrets = new List<Secret> {new Secret("subscribtions_strong_password(!)".Sha256())}, 
                     //UserClaims = new List<string> {"role"}
                 },
                 new ApiResource
                 {
                     Name = "reporting",
                     DisplayName = "Reporting",
                     Description = "Access the Reporting Services",
                     Scopes = new List<string> {"reports.generate"},
                     ApiSecrets = new List<Secret> {new Secret("reporting_strong_password(!)".Sha256())}, 
                     //UserClaims = new List<string> {"role"}
                 }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //UI Client 
                new Client
                {
                    ClientId = "ignite-mobile-admin",
                    ClientName = "IgniteMobileAdmin",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = new List<Secret> {new Secret("ignite-mobile-admin-strong-secret(!)".Sha256())},
                    AllowedScopes = new List<string> { "openid", "profile", "reports.generate" },
                    RedirectUris = new List<string> { "https://localhost:44384/signin-oidc" }
                },
                new Client
                {
                    ClientId = "ignite-mobile-customer",
                    ClientName = "IgniteMobileCustomer",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = new List<Secret> {new Secret("ignite-mobile-customer-strong-secret(!)".Sha256())},
                    AllowedScopes = new List<string> { "openid", "profile", "subscriber.read" },
                    RedirectUris = new List<string> { "https://localhost:44370/signin-oidc" }
                },

                //API client
                new Client
                {
                    ClientId = "reporting",
                    ClientName = "Reporting",
                    // The Client Credentials grant type is used by clients to obtain an access token outside of the context of a user (server-to-server)
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("reporting_strong_password(!)".Sha256())},
                    // could be an: api resource, 
                    AllowedScopes = new List<string> { "subscribers.read" },
                    RedirectUris = new List<string> { "https://localhost:6001/signin-oidc" }
                }
            };
        }


        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }
        
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser> {
                new TestUser {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "beees",
                    Password = "password",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Email, "bflasayed@gmail.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}