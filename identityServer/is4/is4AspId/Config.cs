﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace is4AspId
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1"),                
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("api1", "WeatherForecast api")
                {
                    Scopes = {"api1"},
                    ApiSecrets =  { new Secret("amarillo".Sha256()) }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "swaggerUI",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,  
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "https://localhost:44313/swagger/oauth2-redirect.html" },
                    

                    AllowedCorsOrigins = { "https://localhost:44313" },
                    AllowedScopes = { "api1" },                    
                    
                },
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "BlazorApp",
                    ClientName = "Bazoa web app",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    ClientUri = "https://localhost:44361",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AlwaysIncludeUserClaimsInIdToken = true,

                    EnableLocalLogin = true,
                    RequireConsent = false,
                    
                    AllowOfflineAccess = false,                                        
                    IdentityTokenLifetime = 10,
                    AccessTokenLifetime = 10, 
                    UserSsoLifetime = 10,                       
                    
                    
                    

                    //where to redirect to after login                
                    RedirectUris = { "https://localhost:44361/signin-oidc" },
                    //where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:44361/signout-callback-oidc" },
                    //FrontChannelLogoutUri = "https://localhost:44361/signout-oidc",



                    AllowedCorsOrigins = { "https://localhost:44361" },
                    AllowedScopes = { "openid", "profile", "api1" },

                },
            };
    }
}