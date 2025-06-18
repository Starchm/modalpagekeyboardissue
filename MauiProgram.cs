using System.Diagnostics;
using AndroidX.Activity;
using AndroidX.Fragment.App;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;

namespace modalpagekeyboardissue
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
#if ANDROID
                .ConfigureLifecycleEvents( o =>
                {
                    o.AddAndroid(ao =>
                    {
                        ao.OnCreate((a, si) =>
                        {
                            if (a is ComponentActivity ca)
                            {
                                ca.GetFragmentManager().RegisterFragmentLifecycleCallbacks(new CustomFragmentLifecycleCallbacks(), false);
                            }
                        });
                    });
                })
#endif
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
                

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }

#if ANDROID
    public class CustomFragmentLifecycleCallbacks: FragmentManager.FragmentLifecycleCallbacks
    {
        public override void OnFragmentStarted(FragmentManager? fm, Fragment? f)
        {
            if (f is DialogFragment df)
            {
                df.Dialog.KeyPress += Dialog_KeyPress;

            }
            base.OnFragmentStarted(fm, f);  
        }

        private void Dialog_KeyPress(object? sender, Android.Content.DialogKeyEventArgs e)
        {
            //Debugger.Break();
        }
    }
#endif
}
