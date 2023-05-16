using Foundation;
using Avalonia;
using Avalonia.iOS;
using Microsoft.Maui.ApplicationModel;
using Samples;

namespace Essentials.Sample.iOS;

// The UIApplicationDelegate for the application. This class is responsible for launching the 
// User Interface of the application, as well as listening (and optionally responding) to 
// application events from iOS.
[Register("AppDelegate")]
public partial class AppDelegate : AvaloniaAppDelegate<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
	        .AfterSetup(_ => Platform.Init(() => Window.RootViewController!))
            .WithInterFont();
    }
}
