namespace APIGateway.Contracts
{
    public sealed class AppSetttings
    {
        public SwaggerSettings Swagger { get; set; }
        public sealed class SwaggerSettings
        {
            public bool UIRendering { get; set; }
            public string PathToSwaggerGenerator { get; set; }
            public string DownstreamSwaggerEndPointBasePath { get; set; }
        }
        public CorsSettings Cors { get; set; }
        public sealed class CorsSettings
        {
            public string[] Origins { get; set; }
            public string[] Headers { get; set; }
            public string[] Methods { get; set; }
        }
    }
}
