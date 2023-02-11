using Bit.OData.Implementations;
using Microsoft.AspNet.OData.Builder;
using System.Web.Http;

namespace Boomrang.App.Helper
{
    public class BoomrangODataModelBuilderProvider : DefaultODataModelBuilderProvider
    {
        public override ODataModelBuilder GetODataModelBuilder(HttpConfiguration webApiConfig, string containerName, string? @namespace)
        {
            ODataModelBuilder builder = base.GetODataModelBuilder(webApiConfig, containerName, @namespace);

            return builder;
        }
    }
}
