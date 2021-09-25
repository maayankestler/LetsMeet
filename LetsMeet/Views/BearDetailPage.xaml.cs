﻿using System;
using System.Linq;
using Xamarin.Forms;
using LetsMeet.Data;
using LetsMeet.Models;

namespace LetsMeet.Views
{
    [QueryProperty(nameof(Name), "name")]
    public partial class BearDetailPage : ContentPage
    {
        public string Name
        {
            set
            {
                LoadAnimal(value);
            }
        }

        public BearDetailPage()
        {
            InitializeComponent();
        }

        void LoadAnimal(string name)
        {
            try
            {
                Animal animal = BearData.Bears.FirstOrDefault(a => a.Name == name);
                BindingContext = animal;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load animal.");
            }
        }
    }
}
