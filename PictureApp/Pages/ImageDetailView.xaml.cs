using PictureApp.Models;

namespace PictureApp.Pages;

public partial class ImageDetailView : ContentPage
{
    private readonly ImageItem _selectedImage;

    public ImageDetailView(ImageItem selectedImage) {
        InitializeComponent();
        _selectedImage = selectedImage;
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        imageDetail.Source = _selectedImage.ImagePath;
        imageName.Text = _selectedImage.ImageName;
    }
}