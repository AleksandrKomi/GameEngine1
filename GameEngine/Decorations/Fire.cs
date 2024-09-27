using GameEngine.Characters;
using GameEngine.Interfeces;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Decorations
{
    public class Fire : IPhysicalSprite
    {
        private const int FireSize = 30;
        public int X; 
        public int Y = 50;
        private Direction _currentDirection = Direction.Right;
        private Image _fire;
        

        public Fire(Player player)
        {
            // Load resources

            _fire = Image.FromFile("Resources/fire.png");
            
            X = player.X;
            Y = player.Y + 50;
            _currentDirection = player.CurrentDirection;           
        }

        public void Draw(Graphics g, Rectangle bounds)
        {
            g.DrawImage(_fire, X + 20, bounds.Bottom - Y - FireSize, FireSize, FireSize);
        }

        public void ProcessPhysics()
        {           
            MoveDown();
        }

        public void MoveDown()
        {
            if (_currentDirection == Direction.Right)
            {             
                X += 8;
            }
            else if (_currentDirection == Direction.Left)
            {
                X -= 8;
            }

        }
    }
}
