namespace Northwind.WebUI.Settings
{
    public class SwaggerSettings
    {
        public Doc SwaggerDoc { get; set; }
        public Endpoint SwaggerEndpoint { get; set; }

        public class Doc
        {
            public string Name { get; set; } = "v1";

            public SwaggerOpenApiInfo OpenApiInfo { get; set; }

            public class SwaggerOpenApiInfo
            {
                public string Title { get; set; } = "Northwind API";
                public string Version { get; set; } = "v1";
            }
        }

        public class Endpoint
        {
            public string Name { get; set; } = "Northwind API";
            public string Url { get; set; } = "/swagger/v1/swagger.json";
        }
    }
}
