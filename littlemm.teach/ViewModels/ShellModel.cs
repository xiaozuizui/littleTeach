using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace littlemm.teach.ViewModels
{
    [ImplementPropertyChanged]
    public class ShellModel
    {
        public static ShellModel Current { get; set; }

        public Frame Frame { get; set; }
        public MenuItem SelectItem { get; set; }
        public ObservableCollection<MenuItem> Items { get; }
        public bool isMenuOpen { get; set; }
        public MenuItem SelectedItem { get; set; }


        public ShellModel(Frame frame)
        {
            Frame = frame;
            Items = new ObservableCollection<MenuItem>(new[]
            {

                new MenuItem
                {
                    Name = "ClassTable",
                    Icon = "\uE10F",
                    Target = typeof(Views.ClassTable)
                },

                new MenuItem
                {
                    Name = "StudentInfo",
                    Icon = "\uE778",
                   // Target = typeof(StudentInfo)
                }
              
            });

            SelectedItem = Items[0];
            Current = this;
        }

        public void ToggleMenu()
        {
            isMenuOpen = !isMenuOpen;
        }
        public void OnSelectedItemChanged() => Navigate(SelectedItem.Target);
        public void Navigate(Type target) => Frame.Navigate(target);
    }
    [ImplementPropertyChanged]
    public class MenuItem
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public Type Target { get; set; }
    }
}
