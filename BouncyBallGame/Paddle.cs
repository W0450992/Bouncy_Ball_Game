using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBallGame
{
    public class Paddle
    {
        //Properties
        private readonly int width = 100;
        private readonly int height = 20;
        private readonly int xVelocity = 15;
        public Rectangle DisplayRect;
        private Rectangle Canvas;

        public enum Direction { Left, Right }
        /// <summary>
        /// This is the constructor of the class Paddle
        /// </summary>
        public Paddle(Rectangle canvas)
        {
            this.Canvas = canvas;
            int paddleX = (canvas.Width/2) - (int) (width/2);
            int paddleY = canvas.Bottom - (int) (canvas.Height*0.1);
            DisplayRect = new Rectangle(paddleX, paddleY, width, height); 
        }
        public void Move(Direction directions)
        {
            switch (directions)
            {
                case Direction.Left:
                    if (this.DisplayRect.X <= Canvas.Left)
                    {
                        this.DisplayRect.X = 0;
                    }
                    else
                    {
                        this.DisplayRect.X -= xVelocity;
                    }
                    

                    break;
                case Direction.Right:
                    int maxValue = Canvas.Width - DisplayRect.Width;
                    if (DisplayRect.X >= maxValue)
                    {
                        this.DisplayRect.X = maxValue;
                    }
                    else
                    {
                        this.DisplayRect.X += xVelocity;
                    }
                    
                    break;
            }
        }
        internal void Draw(Graphics graphics)
        {
            //Rectangle rec = new Rectangle(50, 200, width, height);
            //graphics.DrawRectangle(new Pen(Brushes.White), rec);
            graphics.FillRectangle(Brushes.White, DisplayRect);


        }
        
    }
}
