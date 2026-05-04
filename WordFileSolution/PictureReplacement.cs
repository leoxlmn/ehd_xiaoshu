
namespace WordFileSolution {
    public class PictureReplacement : IReplacement {
        public string ImagePath { get; set; }

        public PictureReplacement(string imagePath) {
            ImagePath = imagePath;
        }
    }
}
