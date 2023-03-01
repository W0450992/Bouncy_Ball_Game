using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBallGame
{
    public class Ball
    {
        //Properties
        
        public Rectangle DisplayArea; // represents the ball 

        public int currentX
        {
            get { return DisplayArea.X; }
        }
        public int currentY
        {
            get { return DisplayArea.Y; }
        }

        public int Size { get { return this.size; } }

        private Rectangle Canvas;
        private readonly int size = 50;
        private int xVelocity;
        private int yVelocity;
        private Image image;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="canvas"></param>
        public Ball(Rectangle canvas)
        {
            image = Image.FromFile("images/ball.png");
            this.Canvas = canvas;
            DisplayArea.Height = size;
            DisplayArea.Width = size;
            // Display the ball at the centre of the screen
            DisplayArea.X = (canvas.Width / 2) - (int)(DisplayArea.Width / 2);
            DisplayArea.Y = (canvas.Height /2) - (int)(DisplayArea.Height / 2);
            Random random = new Random();
            //xVelocity = random.Next(-15, 15);
            //yVelocity = random.Next(-15, 15);
            while (xVelocity >-3 && xVelocity < 3)
            {
                xVelocity = random.Next(-15, 15);
            }
            while (yVelocity > -3 && yVelocity < 3)
            {
                yVelocity = random.Next(-15, 15);
            }

        }
        public void Move()
        {
            DisplayArea.X += xVelocity;
            DisplayArea.Y += yVelocity;


        }
        internal void Draw(Graphics graphics)
        {
            //graphics.FillEllipse(Brushes.White, DisplayArea);
            graphics.DrawImage(image, DisplayArea);
        }
        public void FlipX()
        {
            xVelocity *= -1;
        }
        public void FlipY()
        {
            yVelocity *= -1;
        }

    }
}
