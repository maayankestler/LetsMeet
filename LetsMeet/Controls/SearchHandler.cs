using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using LetsMeet.Models;

namespace LetsMeet.Controls
{

    public class UsersSearchHandler : SearchHandler
    {
        public IList<User> Users { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Users
                    .Where(user => user.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList<User>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            string route = GetNavigationTarget();
            string userId = (item as User).Id;
            await Shell.Current.GoToAsync($"{route}?id={userId}");
        }

        string GetNavigationTarget()
        {
            return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
        }
    }
}
