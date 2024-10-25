using Foundation;
using Avalonia.iOS;
using UIKit;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Fabulous.Avalonia;

namespace Theme.iOS
{

    [Register("AppDelegate")]
    public partial class AppDelegate : UIResponder, IUIApplicationDelegate
    {
        [Export("application:configurationForConnectingSceneSession:options:")]
        public UISceneConfiguration GetConfiguration(UIApplication ap, UISceneSession sceneSession, UISceneConnectionOptions op)
        {
            return UISceneConfiguration.Create("Default Configuration", sceneSession.Role);
        }
    }

    [Register("SceneDelegate")]
    public class SceneDelegate : UIWindowSceneDelegate
    {
        public override UIWindow? Window { get; set; }

        public override void WillConnect(UIScene scene, UISceneSession session, UISceneConnectionOptions connectionOptions)
        {
            var lt = new SingleViewLifetime();

            AppBuilder.Configure<App>()
                .UseiOS()
                .AfterSetup(x =>
                {
                    var view = new AvaloniaView();
                    lt.View = view;

                    var win = new UIWindow((UIWindowScene)scene);
                    var con = new DefaultAvaloniaViewController() { View = view };
                    win.RootViewController = con;
                    view.InitWithController(con);
                    this.Window = win;
                    win.MakeKeyAndVisible();
                })
                .With(new iOSPlatformOptions() { RenderingMode = [iOSRenderingMode.Metal, iOSRenderingMode.OpenGl] })
                .SetupWithLifetime(lt);
        }
    }
}