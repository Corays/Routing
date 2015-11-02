// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// Extension methods for adding the <see cref="RouterMiddleware"/> middleware to an <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class RoutingBuilderExtensions
    {
        /// <summary>
        /// Adds a <see cref="RouterMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/> with the specified <see cref="IRouter"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <param name="router">The <see cref="IRouter"/> to use for routing requests.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseRouter(this IApplicationBuilder builder, IRouter router)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (router == null)
            {
                throw new ArgumentNullException(nameof(router));
            }

            return builder.UseMiddleware<RouterMiddleware>(router);
        }

        public static IRouteBuilder UseRouter(this IApplicationBuilder builder)
        {
            return builder.UseRouter(defaultHandler: null);
        }

        public static IRouteBuilder UseRouter(this IApplicationBuilder builder, IRouteEndpoint defaultHandler)
        {
            return new RouteBuilder()
            {
                ConstraintResolver = builder.ApplicationServices.GetRequiredService<IInlineConstraintResolver>(),
                DefaultHandler = defaultHandler,
                ServiceProvider = builder.ApplicationServices,
            };
        }
    }
}