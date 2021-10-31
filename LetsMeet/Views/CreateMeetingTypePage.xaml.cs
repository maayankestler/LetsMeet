using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LetsMeet.Data;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateMeetingTypePage : ContentPage
    {
        public string Name { get; set; }
        public string IconURL { get; set; }
        private string _categoryId = "1";
        public MeetingCategory Category
        {
            get
            {
                return MeetingCatogriesData.GetCategory(_categoryId);
            }
            set
            {
                _categoryId = value.Id;
            }
        }
        public CreateMeetingTypePage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        async private void create_button_clicked(object sender, EventArgs e)
        {
            MeetingType m = new MeetingType("10", Name, IconURL, _categoryId); // TODO change Id hardcoded
            MeetingTypesData.CreateMeetingType(m);
            await Shell.Current.GoToAsync("//CreateMeeting");
        }
    }
}