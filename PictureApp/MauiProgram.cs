using Microsoft.Extensions.Logging;
using PictureApp.CustomControls;
using PictureApp.Models.ViewModels;
using PictureApp.Platforms;

namespace PictureApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            Microsoft.Maui.Handlers.ElementHandler.ElementMapper.AppendToMapping("Classic", (handler, view) => {
                if (view is LoginEntry) {
                    EntryMapper.Map(handler, view);
                }
            });

            return builder.Build();
        }
    }
}