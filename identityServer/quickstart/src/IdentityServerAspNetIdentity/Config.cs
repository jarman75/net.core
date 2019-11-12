// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServerAspNetIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),               
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api1", "my api"),                                
                new ApiResource("roles", "my Roles", new [] {"role"})
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1", "roles" }
                },

                // MVC client using code flow + pkce
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    
                    //RequirePkce = true,
                    RequireConsent = false,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    //where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    
                    FrontChannelLogoutUri = "http://localhost:5002/signout-oidc",
                    
                    //where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1", "roles" }
                },

                //Blazor client using code flow + pkce
                new Client
                {
                    ClientId = "blazor",
                    ClientName = "Blazor server app",

                    //AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    
                    //RequirePkce = true,
                    RequireConsent = false,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,

                    //where to redirect to after login
                    RedirectUris = { "http://localhost:5004/signin-oidc" },
                    
                    FrontChannelLogoutUri = "http://localhost:5004/signout-oidc",
                    
                    //where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5004/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1", "roles" }
                    
                },

                
            };
    }
}