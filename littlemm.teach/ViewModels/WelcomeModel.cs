using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using littlemm.teach.Controls;
using PropertyChanged;
namespace littlemm.teach.ViewModels
{

    [ImplementPropertyChanged]
    public class WelcomeModel
    {
       
        private const string ImageBase = @"ms-appx:///Assets/Welcome/";
        
       
        public WelcomeModel()
        {
            WelcomeItems = new ObservableCollection<WelcomeItem>(new[]
            {
                new WelcomeItem(
                    "ZZ",
                    "The easiest way to organize lunch with your friends, family, or coworkers.",
                    ImageBase + "DLUTlogo.png"),

                new WelcomeItem(
                    "Organizing made easy",
                    "Hungry? Avoid endless back-and-forth emails or texts. Simply tap 'New Lunch' " +
                    "on the main menu and pick a who, where, and when. We'll take care of the rest.",
                    ImageBase + "DLUTlogo.png",
                    async () => await new SignUpDialog(this).ShowAsync()),
                 
                //new WelcomeItem(
                //    "Discover local delights",
                //    "When you're scheduling a new lunch, Lunch Scheduler can use your current location to " +
                //    "suggest tasty restaurants nearby.",
                //    ImageBase + "Location.png",
                //    async () => await DispatchHelper.RunAsync(async () => await Geolocator.RequestAccessAsync())),

                //new WelcomeItem(
                //    "Get signed up",
                //    "To keep track of everything, you'll need an account with Lunch Scheduler. You can log in " +
                //    "with an existing Microsoft, Google, or Facebook account. Lunch Scheduler will never post " +
                //    "to your wall or send you unsolicted email. And there's no extra passwords to remember.",
                //    ImageBase + "Keys.png",
                //    AccountsSettingsPane.Show),

                //new WelcomeItem(
                //    "Be notified",
                //    "Invited to lunch? We'll let you know with a push notification or text message." +
                //    " You can see the location and who's invited before you RSVP.",
                //    ImageBase + "Notification.png",
                //    async () => await new PhoneSignUpDialog().ShowAsync()),

                //new WelcomeItem(
                //    "Good to go!",
                //    "You're all set! Enjoy lunching!",
                //    ImageBase + "Check.png",
                //    () => Window.Current.Content = new Views.Shell()),

                //new WelcomeItem("", "", "")
            });

            SelectedWelcomeItem = WelcomeItems[0];
        }

       
             public ObservableCollection<WelcomeItem> WelcomeItems { get; }

       
            public WelcomeItem SelectedWelcomeItem { get; set; }

       
            public bool IsEnabled { get; set; } = true;

        
        private void OnSelectedWelcomeItemChanged()
        {
            IsEnabled = false;
            if (null != WelcomeItems)
            {
                int index = WelcomeItems.IndexOf(SelectedWelcomeItem) ;
                if (index >= 0 && null != WelcomeItems[index]?.OnNavigatedFrom)
                {
                    WelcomeItems[index].OnNavigatedFrom();


                }

                //await Task.Run(async () =>
                //{
                //    await Task.Delay(1000);
                //    await DispatchHelper.RunAsync(() => IsEnabled = true);
                //});
            }
        }

        //    /// <summary>
        //    /// Make text larger or smaller depending on window size.
        //    /// </summary>
        //public void ChangeFontSize() => SelectedWelcomeItem.ContentFontSize =
        //    Window.Current.Bounds.Width < App.NarrowWidth ? App.FontSizeSmall : App.FontSizeMedium;
    }


        public class WelcomeItem
    {

        public WelcomeItem(string headerText, string contentText,
            string image, Action onNavigatedFrom = null)
        {
            HeaderText = headerText;
            ContentText = contentText;
            if (!String.IsNullOrEmpty(image))
            {
                Image = new Uri(image);
            }
            OnNavigatedFrom = onNavigatedFrom;

            //make text larger or smaller depending on window size
            //if (window.current.bounds.width < app.narrowwidth)
            //{
            //    contentfontsize = app.fontsizesmall;
            //}
            //else
            //{
            //    contentfontsize = app.fontsizemedium;
            //}
        }


        public string ContentText { get; set; } 
        public double ContentFontSize { get; set; }
        public string HeaderText { get; set; }
        public Uri Image { get; set; }
        public Action OnNavigatedFrom { get; set; }
    }
}