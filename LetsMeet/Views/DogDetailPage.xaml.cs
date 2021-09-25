using System;
using System.Linq;
using Xamarin.Forms;
using LetsMeet.Data;
using LetsMeet.Models;

namespace LetsMeet.Views
{
    [QueryProperty(nameof(Name), "name")]
    public partial class DogDetailPage : ContentPage
    {
        public string Name
        {
            set
            {
                LoadAnimal(value);
            }
        }

        public DogDetailPage()
        {
            InitializeComponent();
        }

        void LoadAnimal(string name)
        {
            try
            {
                Animal animal = DogData.Dogs.FirstOrDefault(a => a.Name == name);
                BindingContext = animal;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load animal.");
            }
        }
    }
}
