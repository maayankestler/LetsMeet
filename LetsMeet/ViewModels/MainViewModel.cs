using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using LetsMeet.Data;
//using Navigation.NavigationStack;

namespace LetsMeet.ViewModels
{
    class MainViewModel
    {
        private static MainViewModel instance = null;

        public static MainViewModel GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new MainViewModel();
                return instance;
            }
        }

//        private User _currentUser = null;
        public User CurrentUser { get; private set; }

        async public void LogOut()
        {
            CurrentUser = null;
            // clear navigation stack https://stackoverflow.com/questions/27175079/how-to-restart-the-application-using-xamarin-forms
            (Application.Current).MainPage = new AppShell(); 
            await Shell.Current.GoToAsync("//login");
        }

        public void Login(string UserName, string Password)
        {
            CurrentUser = UsersData.GetUser(UserName, Password);
            // if admin
            if (CurrentUser!= null && CurrentUser.IsAdmin) 
            {
                
                foreach (var item in Shell.Current.Items)
                {
                    if (item.Title == "MainFlyout")
                    {
                        Tab AdminTab = new Tab() { Title = "Admin", Icon = "admin.png" };
                        AdminTab.Items.Add(new ShellContent()
                        {
                            Route = "CreateMeetingType",
                            Title = "Create meeting type",
                            Style = (Style)AppShell.Current.Resources["AdminShell"],
                            Content = new LetsMeet.Views.CreateMeetingTypePage()
                        });
                        AdminTab.Items.Add(new ShellContent()
                        {
                            Route = "CreateMeetingCategory",
                            Title = "Create meeting category",
                            Style = (Style)AppShell.Current.Resources["AdminShell"],
                            Content = new LetsMeet.Views.CreateMeetingCategoryPage()
                        });

                        item.Items.Add(AdminTab);
                    }
                }
            }
        }

        public bool IsLoggedIn()
        {
            return CurrentUser != null;
        }
    }
}
