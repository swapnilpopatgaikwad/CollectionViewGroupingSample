using CollectionViewGroupingSample.View;
using System;
using Xamarin.Forms;

namespace CollectionViewGroupingSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
          await Navigation.PushAsync(new CollectionViewGrouping());
        }
    }
}
