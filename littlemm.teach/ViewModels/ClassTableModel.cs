using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using Windows.UI.Xaml.Controls;
using littlemm.teach.Base;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
namespace littlemm.teach.ViewModels
{
    [ImplementPropertyChanged]
    public class ClassTableModel
    {
        public int Week { get; set; }
        
        public StackPanel MainPanel;
        TextBlock[,] tb;
        N_Student stu = App.stu;

        public ClassTableModel(StackPanel sp)
        {
            Week = 8;
            MainPanel = sp;
            tb = new TextBlock[11, 7];

            for (int i = 0; i < 7; i++)
            {
                StackPanel sp2 = new StackPanel { Orientation = Orientation.Vertical };
                for (int j = 0; j < 11; j++)
                {
                    tb[j, i] = new TextBlock { Height = 50, Width = 180 ,VerticalAlignment = VerticalAlignment.Center};

                    sp2.Children.Add(tb[j, i]);
                }
                MainPanel.Children.Add(sp2);
            }
            setClass(stu.N_Classes.ToArray(), Week);
        }

      //  public void OnSelectedItemChanged() => setClass(stu.N_Classes.ToArray(), Week);

        public void Left_Button_Click()
        {
            Week--;
            setClass(stu.N_Classes.ToArray(), Week);
        }

        public void Right_Button_Click()
        {
            Week++;
            setClass(stu.N_Classes.ToArray(), Week);
        }

        public void setClass(N_Class[] cl, int week)
        {
            setText();
            foreach (N_Class c in cl)
            {
                foreach (Section s in c.ClassSection)
                {
                    if (s.start <= week && s.end >= week)
                    {
                        for (int i = 0; i < s.number; i++)
                        {
                            tb[s.jc + i - 1, s.wk - 1].Text = c.ClassName;
                            tb[s.jc + i - 1, s.wk - 1].AddHandler(UIElement.TappedEvent, new TappedEventHandler(OntappedAsync), true);
                        }
                    }
                }
            }
        }

        public void setText()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    tb[j, i].Text = "";
                    
                    tb[j, i].RemoveHandler(UIElement.TappedEvent, new TappedEventHandler(OntappedAsync));
                }
            }
        }
         
        private async void OntappedAsync(object sender, RoutedEventArgs e)
        {
            TextBlock textb = sender as TextBlock;
            StackPanel sp = new StackPanel { Orientation = Orientation.Vertical };
            TextBlock t = new TextBlock
            {
                Text = textb.Text,
            };
            sp.Children.Add(t);
            //   int k = Convert.ToUInt16(textb.Name);
           
            ContentDialog cd = new ContentDialog()
            {
                Title = "课程详情",
                Content = sp,
                SecondaryButtonText = "确定"

            };

            await cd.ShowAsync();
        }
    }
}
