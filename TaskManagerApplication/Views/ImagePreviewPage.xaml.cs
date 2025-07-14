namespace TaskManagerApplication.Views
{
    public partial class ImagePreviewPage : ContentPage
    {
        public ImagePreviewPage(string imagePath)
        {
            InitializeComponent(); 

            if (File.Exists(imagePath))
                PreviewImage.Source = ImageSource.FromFile(imagePath);
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
