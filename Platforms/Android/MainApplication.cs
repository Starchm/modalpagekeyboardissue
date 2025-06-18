using Android.App;
using Android.Runtime;

namespace modalpagekeyboardissue
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {

            Microsoft.Maui.Handlers.ViewHandler.ViewMapper.AppendToMapping(nameof(FocusableView), (h, v) =>
            {
                if (v is FocusableView focusableView)
                {
                    var cvg = h.PlatformView as PlatformContentViewGroup;
                    cvg.Focusable = true;
                    cvg.FocusableInTouchMode = true;
                    cvg.Clickable = true;
                }
            });
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
