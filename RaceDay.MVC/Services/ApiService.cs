using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace RaceDay.MVC.Services
{
    // Service responsible for communicating with the RaceDay API
    public class ApiService
    {
        // HttpClient used to send requests to the API
        private readonly HttpClient _httpClient;

        // Stores API configuration settings
        private readonly IConfiguration _configuration;

        // Constructor receives configuration from appsettings.json
        public ApiService(IConfiguration configuration)
        {
            _configuration = configuration;

            _httpClient = new HttpClient();

            // Set API base address
            _httpClient.BaseAddress =
                new Uri(
                    _configuration["ApiSettings:BaseUrl"]!);
        }

        // Returns configured HttpClient
        public HttpClient GetClient()
        {
            return _httpClient;
        }
    }
}