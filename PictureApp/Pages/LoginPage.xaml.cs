using Newtonsoft.Json;
using PictureApp.Helpers;
using PictureApp.Models;
using System.Diagnostics;

namespace PictureApp.Pages;

public partial class LoginPage : ContentPage
{
    private bool _isLoggedIn = false;


	public LoginPage()
	{
		InitializeComponent();
        var passwordFile = FileHelper.LoadData();
        if (passwordFile != null) {
            _isLoggedIn = true;
        }
        UpdateWelcomeLabel(_isLoggedIn);
    }

    private async void LoginEntry_Completed(object sender, EventArgs e) {
        if (((Entry)sender).Text.Length != ((Entry)sender).MaxLength) {
            return;
        }
        if (!_isLoggedIn) {
            PasswordOperationHandler.SavePaswordAndSalt(((Entry)sender).Text, PasswordHasher.GenerateRandomSalt());
            await ChangeToGalleryPage();
        }
        else {
            PasswordModel inputPasswordModel = JsonConvert.DeserializeObject<PasswordModel>(FileHelper.LoadData());

            if (PasswordOperationHandler.LoadAndVerifyPasswordAndSalt(((Entry)sender).Text, inputPasswordModel.Salt)) {
                await ChangeToGalleryPage();
            }
            else {
                await DisplayAlert("Неверный пароль!", "Введите пароль снова.", "OK");
                loginEntry.Text = string.Empty;
                return;
            }
        }

    }

    private void UpdateWelcomeLabel(bool loginState) {
        if (!loginState) {
            welcomeLabel.Text = "Создайте пароль из 5 чисел";
        } else {
            welcomeLabel.Text = "Введите ваш пароль";
        }
    }

    private async Task ChangeToGalleryPage() {
        PermissionStatus storageWriteStatus = await CheckPermissions<Permissions.StorageWrite>();
        PermissionStatus storageReadStatus = await CheckPermissions<Permissions.StorageRead>();
        PermissionStatus photosStatus = await CheckPermissions<Permissions.Photos>();
        var galleryPage = new GalleryPage();
        App.Current.MainPage = new NavigationPage(galleryPage);
        await (App.Current.MainPage as NavigationPage).PushAsync(galleryPage);
    }

    private async Task<PermissionStatus> CheckPermissions<TPermission>() where TPermission : Permissions.BasePermission, new() {
        PermissionStatus status = await Permissions.CheckStatusAsync<TPermission>();

        if (status != PermissionStatus.Granted) {
            status = await Permissions.RequestAsync<TPermission>();
        }

        return status;
    }
}