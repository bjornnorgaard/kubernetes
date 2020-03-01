namespace Api.Web.Options
{
    public class CorsOptions
    {
        public string[] AllowedOrigins { get; set; }

        public CorsOptions()
        {
            AllowedOrigins = new string[] { };
        }
    }
}



