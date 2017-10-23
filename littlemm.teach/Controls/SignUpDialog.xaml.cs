using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using littlemm.teach.Base;
using littlemm.teach.Model;
using littlemm.teach.ViewModels;
// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace littlemm.teach.Controls
{
    public sealed partial class SignUpDialog : ContentDialog
    {
        string username;
        string password;
        WelcomeModel wm;
        public SignUpDialog(WelcomeModel p)
        {
            this.InitializeComponent();
            wm = p;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }

        private async void ContentDialog_SecondaryButtonClickAsync(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            bool isCheck = true;
            username = UserName.Text.ToString();
            password = PassWord.Password.ToString();
            pg.IsActive = true;

            Net.Teachnet teachnet = new Net.Teachnet(username, password);
            teachnet.initializeAsync();
            //   IAsyncOperation<string> iasync = teachnet.initializeAsync().Result;
            //   iasync.Completed = TeachnetCompleted;
            int t;
            t = 0;
            while (teachnet.classinfo == null&&t<3)
            {
                await Task.Delay(2000);
                t++;
            }

            StToInfo saveToInfo = new StToInfo(teachnet.classinfo);
            saveToInfo.SaveTodb();
            using (var db = new ClassDb())
            {
                if (db.Students.LongCount() != 0)
                {
                    SaveToRam Str = new SaveToRam();
                    Str.Analysis();
                }
            }

            pg.IsActive = false;
            args.Cancel = isCheck;
            Frame f = Window.Current.Content as Frame;
            
            f.Navigate(typeof(Views.ShellView));
            
        }

        void TeachnetCompleted(IAsyncOperation<string> async,AsyncStatus asyncStatus)
        {
            String st = async.GetResults();
            StToInfo sttoinfo = new StToInfo(st);
            sttoinfo.SaveTodb();
            using (var db = new ClassDb())
            {
                if (db.Students.LongCount() != 0)
                {
                    SaveToRam Str = new SaveToRam();
                    Str.Analysis();
            
                }
            }
            pg.IsActive = false;
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            //username = UserName.Text.ToString();
            //password = PassWord.Password.ToString();
            username = "201585081";
            password = "19970401";
            pg.IsActive = true;

            Net.Teachnet teachnet = new Net.Teachnet(username, password);
           // teachnet.initializeAsync();
            //   IAsyncOperation<string> iasync = teachnet.initializeAsync().Result;
            //   iasync.Completed = TeachnetCompleted;
            await Task.Delay(2000);

           // StToInfo saveToInfo = new StToInfo(teachnet.classinfo);
            //saveToInfo.SaveTodb();
            using (var db = new ClassDb())
            {
                if (db.Students.LongCount() != 0)
                {
                    SaveToRam Str = new SaveToRam();
                    Str.Analysis();

                }
            }
            
            pg.IsActive = false;
            Frame f = Window.Current.Content as Frame;
            f.Navigate(typeof(Views.ShellView));
            Hide();
            //  rootFrame.Navigate(typeof(Views.ShellView), e.Arguments);
        }
    }
}
