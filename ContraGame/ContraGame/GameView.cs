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

            int heroX = Model.Hero.X;
            int centerX = (int)(Canvas.Width / 2);
            int diffX = centerX - heroX;

            foreach(Tile tile in Model.GetTiles())
            {
                if (tile.Name == "") continue;

                Tile modifiedTile = new Tile(tile.Code, tile.X + diffX, tile.Y, tile.Name);

                WriteImageToCanvas(modifiedTile);
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
