using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBallGame
{
    internal class Box
    {


        private readonly int width = 100;
        private readonly int height = 20;
        private int xVelocity = 0;
        //private readonly int xVelocity = 10;
        public Rectangle DisplayRect;
        private Rectangle Canvas;

        //public enum Direction { Left, Right }
        /// <summary>
        /// This is the constructor of the class Paddle
        /// </summary>
        public Box(Rectangle canvas)
        {
            //var Boxes = new List<dynamic>();
            Random rnd = new Random();
            int randomX = rnd.Next(400, 3200);  // creates a number between 400 and 3200
            int randomY = rnd.Next(200, 800);  // creates a number between 200 and 800
            this.Canvas = canvas;
            int appWidth = canvas.Width - 400;
            int boxX = (canvas.Width / 2); //- (int)(width / 2);
            int boxY = canvas.Top + (int)(canvas.Height * 0.1);
           
            //for (int x = 100; x < appWidth; x+=200)
            //{
            //DisplayRect = new Rectangle(appWidth, boxY, width, height);
            DisplayRect = new Rectangle(randomX , randomY, width, height);
            //Boxes.Add(DisplayRect);
            //}
            
        }
        
        internal void Draw(Graphics graphics)
        {
            //Rectangle rec = new Rectangle(50, 200, width, height);
            //graphics.DrawRectangle(new Pen(Brushes.White), rec);
            graphics.FillRectangle(Brushes.White, DisplayRect);


        }
        public void MoveBox() // move function for moving box out of form
        {
            DisplayRect.X += 4000;
            


        }

        // create a move function
    }
}
