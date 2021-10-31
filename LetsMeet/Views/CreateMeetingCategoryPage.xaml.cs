using LetsMeet.Data;
using LetsMeet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateMeetingCategoryPage : ContentPage
    {
        public string Name { get; set; }
        public string IconURL { get; set; }
        public CreateMeetingCategoryPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        async private void create_button_clicked(object sender, EventArgs e)
        {
            MeetingCategory m = new MeetingCategory("4", Name, IconURL); // TODO change Id hardcoded
            MeetingCatogriesData.CreateMeetingCategory(m);
            await Shell.Current.GoToAsync("//CreateMeetingType");
        }
    }
}