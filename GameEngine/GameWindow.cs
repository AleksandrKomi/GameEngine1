using GameEngine.Characters;
using GameEngine.Decorations;
using GameEngine.Interfeces;
using System.ComponentModel;
using System.Drawing;

namespace GameEngine
{
    public partial class GameWindow : Form
    {
        private readonly System.Timers.Timer _physicsUpdateTimer;
        private readonly BufferedGraphics _bufferedGraphics;

        private int _framesCount;
        private int _fps;

        private readonly Player _player;
        private readonly Opponent _opponent;
        private readonly Land _land;
        private readonly List<ISprite> _sprites;
        
        // Construcor

        public GameWindow()
        {
            InitializeComponent();
            _bufferedGraphics = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), DisplayRectangle);
            _physicsUpdateTimer = new System.Timers.Timer(10);
            _physicsUpdateTimer.Elapsed += _physicsUpdateTimer_Elapsed;

            _player = new Player("Stiv");
            _opponent = new Opponent();
            _land = new Land();
            _sprites = new List<ISprite>();
            _sprites.Add(_player);
            _sprites.Add(_opponent);
            _sprites.Add(_land);
           

            // Start timers

            UpdateScreenTimer.Start();
            FPSCounterTimer.Start();
            _physicsUpdateTimer.Start();
        }

        // Methods
                
        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Left)
            {
                 _player.MoveLeft();
            }
            else if (e.KeyCode == Keys.Right)
            {
                _player.MoveRight();
            }
            else if (e.KeyCode == Keys.Up)
            {
                _player.MoveUp();
            }
            else if (e.KeyCode == Keys.Down)
            {
                Fire fire = new Fire(_player);
                _sprites.Add(fire);
            }
            
        }
        private void ProcessPhysics()
        {     
            // Вызывается 100 раз в сек.

            ISprite[] sprites = _sprites.ToArray();

            foreach (ISprite sprite in sprites)
            {
                if (sprite is IPhysicalSprite physicalSprite)
                {
                    physicalSprite.ProcessPhysics();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Prepare

            Graphics graphics = _bufferedGraphics.Graphics;
            graphics.Clear(Color.Azure);

            Font fpsFont = new Font("Ariel", 16);

            ISprite[] sprites = _sprites.ToArray();

            foreach (ISprite sprite in sprites)
            {
                sprite.Draw(graphics, DisplayRectangle);
            }   

            // Draw FPS

            graphics.DrawString($"FPS: {_fps}", fpsFont, Brushes.Red, DisplayRectangle.Right - 100, 10);

            // Render

            _bufferedGraphics.Render();

            _framesCount++;

        }

        #region Timers

        private void UpdateScreenTimer_Tick(object sender, EventArgs e)
        {
            InvokePaint(this, null);
        }

        private void FPSCounterTimer_Tick(object sender, EventArgs e)
        {
            _fps = _framesCount;
            _framesCount = 0;
        }

        private void _physicsUpdateTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            ProcessPhysics();
        }

        #endregion
    }
}