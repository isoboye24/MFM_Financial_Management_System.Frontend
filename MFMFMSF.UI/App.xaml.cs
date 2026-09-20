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
        public static IGivingService GivingService { get; } = new GivingService(HttpClient);
        public static IExpenditureService ExpenditureService { get; } = new ExpenditureService(HttpClient);
        public static IPositionService PositionService { get; } = new PositionService(HttpClient);

        private static HttpClient CreateHttpClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri("http://192.168.0.195:7200/")
            };
        }
    }
}