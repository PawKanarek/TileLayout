using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TileLayout
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var tileLayout = new TileLayout();
            tileLayout.WidthRequest = 200;
            tileLayout.HeightRequest = 150;
            tileLayout.CoverImage = new BoxView() { BackgroundColor = Color.Red, WidthRequest = 200, HeightRequest = 75 };
            tileLayout.CoverPoster = new Label() { BackgroundColor = Color.Blue, WidthRequest = 150, HeightRequest = 100 };
            tileLayout.Title = new Label() { Text =  "title"};
            tileLayout.Description = new Label() { Text = "Description" };

            this.Content = tileLayout;
        }
    }
}
