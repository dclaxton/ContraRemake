using System.IO;
using System.Text;
using System.Windows;

namespace NotContra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameModel model;
        private Terrain terrain;
        private GameView view;

        public MainWindow()
        {
            InitializeComponent();
            world.Focus();

            BuildTerrain();
            this.model = new GameModel(this.terrain);
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
    }
}
