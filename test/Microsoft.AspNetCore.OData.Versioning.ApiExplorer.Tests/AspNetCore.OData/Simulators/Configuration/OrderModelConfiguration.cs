﻿namespace Microsoft.AspNetCore.OData.Simulators.Configuration
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Simulators.Models;
    using Microsoft.OData.ModelBuilder;

    /// <summary>
    /// Represents the model configuration for orders.
    /// </summary>
    public class OrderModelConfiguration : IModelConfiguration
    {
        /// <inheritdoc />
        public void Apply( ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix )
        {
            var order = builder.EntitySet<Order>( "Orders" ).EntityType.HasKey( o => o.Id );

            if ( apiVersion == ApiVersion.Neutral )
            {
                order.Collection.Function( "MostExpensive" ).ReturnsFromEntitySet<Order>( "Orders" );
                order.Action( "Rate" ).Parameter<int>( "rating" );
                return;
            }

            if ( apiVersion < ApiVersions.V2 )
            {
                order.Ignore( o => o.EffectiveDate );
            }

            if ( apiVersion < ApiVersions.V3 )
            {
                order.Ignore( o => o.Description );
            }

            if ( apiVersion >= ApiVersions.V1 )
            {
                order.Collection.Function( "MostExpensive" ).ReturnsFromEntitySet<Order>( "Orders" );
            }

            if ( apiVersion >= ApiVersions.V2 )
            {
                order.Action( "Rate" ).Parameter<int>( "rating" );
            }
        }
    }
}