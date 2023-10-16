using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PictureApp.Helpers;
using PictureApp.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureApp.Models.ViewModels
{
    public partial class GalleryPageViewModel : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        public ObservableCollection<ImageItem> imageItems;
        private Task _initializeTask;
        public INavigation Navigation { get; set; }

        public GalleryPageViewModel(INavigation navigation) {
            this.Navigation = navigation;
            _initializeTask = GetPhotosPermission();
            LoadGallery();
        }

        

        private void LoadGallery() {
            ImageItems = new ObservableCollection<ImageItem>();
            var imageDirectory = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
            if (Directory.Exists(imageDirectory)) {
                var imageFiles = Directory.GetFiles(imageDirectory);
                foreach (var imagePath in imageFiles) {
                    var imageName = Path.GetFileName(imagePath);
                    var creationTime = File.GetCreationTime(imagePath);
                    ImageItems.Add(new ImageItem { ImagePath = imagePath, ImageName = imageName, CreationTime = creationTime });
                }
            }
        }

        private async Task GetPhotosPermission() {
            var status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status == PermissionStatus.Granted) {
                return;
            } else {
                await Permissions.RequestAsync<Permissions.StorageRead>();
            }
        }

        [RelayCommand]
        private void DeleteButtonClicked(ImageItem imageItem) {
            if (imageItem is null) {
                return;
            }
            if (File.Exists(imageItem.ImagePath)) {
                try {
                        ImageItems.Remove(imageItem);
                        File.Delete(imageItem.ImagePath);
                }
                catch (IOException ex) {
                    // Файл заблокирован другим процессом или приложением
                    Console.WriteLine("Файл заблокирован: " + ex.Message);
                }
                catch (Exception ex) {
                    // Обработка других исключений
                    Console.WriteLine("Произошла ошибка: " + ex.Message);
                }
            }
        }

        [RelayCommand]
        async Task OpenButtonClicked(ImageItem imageItem) {
            if (imageItem is null) {
                return;
            }
            ImageDetailView imageDetailPage = new(imageItem);
            await Navigation.PushAsync(imageDetailPage);
            
        }

    }
}
