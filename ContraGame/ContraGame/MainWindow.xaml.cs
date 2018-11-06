using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace NotContra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Hero hero;
        private GameModel model;
        private Terrain terrain;
        private GameView view;

        public MainWindow()
        {
            InitializeComponent();
            world.Focus();

            BuildTerrain();

            this.hero = new Hero(this.terrain.GetStart());
            this.model = new GameModel(this.terrain, this.hero);
            this.view = new GameView(world, model);

            this.view.Update();
        }

        private void BuildTerrain()
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(LevelResources.level001));

            TerrainBuilder builder = new TerrainBuilder(stream);

            builder.Build();
            this.terrain = builder.Terrain;
        }

        private void world_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.A:
                case Key.Left:
                    this.hero.Left(this.terrain);
                    break;
                case Key.D:
                case Key.Right:
                    this.hero.Right(this.terrain);
                    break;
                case Key.W:
                case Key.Up:
                    this.hero.Jump(this.terrain);
                    break;
            }

            this.view.Update();
        }

        private void world_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.hero.StopRunning();
            this.view.Update();
        }
    }
}
