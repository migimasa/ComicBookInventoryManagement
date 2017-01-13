using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace ComicBookInventoryAPI.Models.Framework
{
    public class ApiHeaderParameter : IOperationFilter
    {
        public string Description { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }

        public void Apply(SwaggerDocsConfig c)
        {
            c.ApiKey(Key).Name(Name).Description(Description).In("header");
            c.OperationFilter(() => this);
        }

        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            operation.parameters = operation.parameters ?? new List<Parameter>();
            operation.parameters.Add(new Parameter
            {
                name = Name,
                description = Description,
                @in = "header",
                required = true,
                type = "string",
                @default = DefaultValue
            });
        }

        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            var security = new List<IDictionary<string, IEnumerable<string>>>
            {
                new Dictionary<string, IEnumerable<string>> { { Key, new string[0]} }
            };
            swaggerDoc.security = security;
        }
    }
}