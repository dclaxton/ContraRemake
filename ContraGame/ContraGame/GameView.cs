using System;
using System.Windows.Controls;

namespace NotContra
{
    class GameView
    {

        public GameView(Canvas canvas, GameModel model)
        {
            if(canvas == null)
            {
                throw new ArgumentNullException("Canvas is null!");
            }

            if(model == null)
            {
                throw new ArgumentNullException("Model is null!");
            }

            Images = new ImageSelector();
            Canvas = canvas;
            Model = model;
        }

        public void Update()
        {
            Canvas.Children.Clear();

            foreach(Tile tile in Model.GetTiles())
            {
                if (tile.Name == "") continue;

                WriteImageToCanvas(tile);
            }

            Canvas.UpdateLayout();
        }

        private void WriteImageToCanvas(Tile tile)
        {
            Image image = new Image();
            image.Source = Images.Get(tile.Name);
            Canvas.SetTop(image, tile.Y);
            Canvas.SetLeft(image, tile.X);
            Canvas.Children.Add(image);
        }

        public Canvas Canvas { get; private set; }
        public ImageSelector Images { get; private set; }
        public GameModel Model { get; private set; }
    }
}
