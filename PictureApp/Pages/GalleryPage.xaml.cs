using PictureApp.Models;
using PictureApp.Models.ViewModels;
using System.Collections.ObjectModel;

namespace PictureApp.Pages;

public partial class GalleryPage : ContentPage
{
	public GalleryPage()
	{
		InitializeComponent();
        this.BindingContext = new GalleryPageViewModel(Navigation);
		
    }
}