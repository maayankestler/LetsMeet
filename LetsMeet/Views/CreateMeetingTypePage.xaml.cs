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
        public string IconURL { get; set; } = "https://png.pngtree.com/png-vector/20190330/ourmid/pngtree-vector-meeting-icon-png-image_894665.jpg";
        private string _categoryId = null;
        public MeetingCategory Category
        {
            get
            {
                MeetingCategory m;
                if (_categoryId == null)
                    m = MeetingCatogriesData.GetFirst();
                else
                    m = MeetingCatogriesData.GetCategory(_categoryId);
                return m;
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
            MeetingType m = new MeetingType(Name, IconURL, _categoryId); // TODO change Id hardcoded
            MeetingTypesData.CreateMeetingType(m);
            await Shell.Current.GoToAsync("//CreateMeeting");
        }
    }
}