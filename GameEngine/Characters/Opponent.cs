using GameEngine.Interfeces;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Characters
{
    internal class Opponent : IPhysicalSprite
    {
        private const int OpponentSize = 75;

        public int X = 500;
        public int Y = 600;
        private Direction _currentDirection = Direction.Left;
        private Image _opponent;
        private Image _opponentLeft;
        private Image _opponentRight;

        public Opponent()
        {
            // Load resources

            _opponent = Image.FromFile("Resources/opponent.png");
            _opponentLeft = Image.FromFile("Resources/opponentLeft.png");
            _opponentRight = _opponent;
        }

        public void Draw(Graphics g, Rectangle bounds)
        {
            g.DrawImage(_opponent, X, bounds.Bottom - Y - OpponentSize - 45, OpponentSize, OpponentSize);
        }

        public void ProcessPhysics()
        {
            // Вызывается 100 раз в сек.

            _opponent = _opponentLeft;

            if (Y > 0)
            {
                Y -= 5;
            }

            if (Y < 45)
            {
                Travel();

                if (X < 300)
                {
                    _currentDirection = Direction.Right;

                    Random rnd = new Random();
                    int z = rnd.Next(300, 750);

                    if (X < z)
                    {
                        Y += 100;
                        X += 50;
                    }
                    else
                    {
                        Y += 100;
                        X += 50;
                    }

                    /*if (X < 300)
                    {
                        
                    }
                    *//*else if (X < 600)
                    {
                        while (Y < 90)
                        {
                            Y += 10;
                            X += 5;
                        }
                    }*/
                }
                else if (X > 750)
                {
                    _currentDirection = Direction.Left;

                    Random rnd = new Random();
                    int z = rnd.Next(300, 750);

                    if (X > z)
                    {
                        Y += 100;
                        X -= 50;
                    }

                    /*if (X > 650)
                    {
                        while (Y < 90)
                        {
                            Y += 10;
                            X -= 5;
                        }
                    }*/
                }
            }
                                                
        }

        public void Travel()
        {
            
            if (_currentDirection == Direction.Left)
            {
                _opponent = _opponentLeft;
                X -= 1;

               /* Random rnd = new Random();
                int z = rnd.Next(300, 750);

                if (X > z)
                {
                    Y += 10;
                    X -= 5;
                }*/

            }
            else if (_currentDirection == Direction.Right)
            {
                _opponent = _opponentRight;
                X += 1;
                X += 1;
            }
        }
    }
}
