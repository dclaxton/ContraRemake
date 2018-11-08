using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System;

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
        private DispatcherTimer gameTimer;

        public MainWindow()
        {
            InitializeComponent();
            world.Focus();

            BuildTerrain();

            this.hero = new Hero(this.terrain.GetStart());
            this.model = new GameModel(this.terrain, this.hero);
            this.view = new GameView(world, model);

            this.view.Update();

            this.gameTimer = CreateTimer(25, true, OnUpdateView);
        }

        DispatcherTimer CreateTimer(int milliseconds, bool enabled, EventHandler callbackMethod)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, milliseconds);
            timer.IsEnabled = enabled;
            timer.Tick += callbackMethod;
            return timer;
        }

        void OnUpdateView(object sender, EventArgs e)
        {
            this.view.Update();
            this.hero.Update(terrain);

            foreach(var projectile in this.hero.Projectiles)
            {
                projectile.Update();
            }
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
                    this.hero.Left();
                    break;
                case Key.D:
                case Key.Right:
                    this.hero.Right();
                    break;
                case Key.W:
                case Key.Up:
                    this.hero.Jump();
                    break;
                case Key.E:
                    this.hero.Shoot();
                    break;
            }

        }

        private void world_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.hero.StopRunning();
        }
    }
}
