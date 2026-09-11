using System.Net.Http;
using System.Windows;
using MFMFMSF.Core.Interfaces;
using MFMFMSF.Infrastructure.Service;

namespace MFMFMSF.UI
{
    public partial class App : Application
    {
        public static HttpClient HttpClient { get; } = CreateHttpClient();

        public static IMeetingCategoryService MeetingCategoryService { get; } = new MeetingCategoryService(HttpClient);
        public static IMeetingService MeetingService { get; } = new MeetingService(HttpClient);
        public static IGivingCategoryService GivingCategoryService { get; } = new GivingCategoryService(HttpClient);

        private static HttpClient CreateHttpClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7002/")
            };
        }
    }
}