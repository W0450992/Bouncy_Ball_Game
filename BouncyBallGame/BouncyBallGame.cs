using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncyBallGame
{

    // more comments at the bottom
    public partial class BouncyBallGame : Form
    {
        // Properties
        int BoxCollisionCounter;
        //Box box;
        Paddle paddle;
        // Declare the ball object. 
        HashSet<Ball> balls = new HashSet<Ball>();
        HashSet<Box> boxes = new HashSet<Box>();
        bool viewInfo = false;
        //private Rectangle Canvas;
        //int centerX  = Canvas.Width / 2;
        //Ball ball;
        private MciPlayer pop = new MciPlayer("sounds/pop.mp3", "1"); // pop sound
        public BouncyBallGame()
        {
            InitializeComponent();
        }

        private void BouncyBallGame_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //Creating the paddle here

            paddle = new Paddle(this.DisplayRectangle);

            //adding new box
            boxes.Add( new Box(this.DisplayRectangle));
            //box = new Box(this.DisplayRectangle);   
            
            
            //ball = new Ball(this.DisplayRectangle);
            balls.Add(new Ball(this.DisplayRectangle));

        }

        private void BouncyBallGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            //Paddle paddle = new Paddle(this.DisplayRectangle);
            paddle.Draw(graphics);
            foreach (var box in boxes) // displays boxes
            {
                box.Draw(graphics);
            }

            //ball.Draw(graphics);
            foreach (var ball in balls)
            {
                ball.Draw(graphics);
            }
            // Display number of balls created
            DisplayNumberOfBalls(graphics);
            DisplayNumberofHits(graphics);
            if (BoxCollisionCounter >= 15) // if you hit a box 15 times then display message
            {
                DisplayYouWin(graphics);
            }
            if (viewInfo == true)
            {
               
                DisplayGameDetails(graphics);
                  
            }
        }

        private void DisplayNumberofHits(Graphics graphics) //  display hits
        {
            string message = $"Hits Number: {BoxCollisionCounter}";
            Font font = new Font(FontFamily.GenericSansSerif, 25);
            graphics.DrawString(message, font, Brushes.Blue, 20, 55);
        }

        private void DisplayNumberOfBalls(Graphics graphics) // balls number
        {
            string message = $"Balls Number: {balls.Count}";
            Font font = new Font(FontFamily.GenericSansSerif, 25);
            graphics.DrawString(message, font,Brushes.Blue, 20, 20);

        }

        private void DisplayYouWin(Graphics graphics) // display message
        {
            string message = $"YOU WIN";
            Font font = new Font(FontFamily.GenericSansSerif, 100);
            graphics.DrawString(message, font, Brushes.Blue, 800, 500);

        }

        private void DisplayGameDetails(Graphics graphics) // display game settings
        {
            string message = $"Made by: Mark Jamieson-Simmons W0450992 \n Left and Right to move \n space to start, B for more boxes \n  N for new ball, esc to exit \n R to restart ";
            Font font = new Font(FontFamily.GenericSansSerif, 25);
            graphics.DrawString(message, font, Brushes.Blue, 20, 85);

        }


        private void BouncyBallGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    paddle.Move(Paddle.Direction.Left); // left arrow calls move function to the left
                   
                    break;
                case Keys.Right:
                    paddle.Move(Paddle.Direction.Right); // right arrow calls move function to the right
                    
                    break;
                case Keys.Space:
                    this.animationTimer.Start(); // to start the timer and perform the ball move
                    
                    break;
                case Keys.Escape:
                    Application.Exit(); // exits the game
                    break;
                case Keys.N:
                    balls.Add(new Ball(this.DisplayRectangle)); // adds a new ball
                    break;
                case Keys.B:
                    boxes.Add(new Box(this.DisplayRectangle)); // adds a new box
                    break;
                case Keys.R:
                    Application.Restart(); // restart on R press
                    break;
                case Keys.I:
                    viewInfo = true; // press i for info about game
                    break;
                    


            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            //Removing any balls that have slipped off the main form
            balls.RemoveWhere(BallMissesPaddle);
            foreach (var ball in balls)
            {
                ball.Move();
            }
            if (balls.RemoveWhere(BallMissesPaddle) > 0)
            {
                pop.PlayFromStart();
            }


            //ball.Move();
            //Check for any collison using this method
            foreach (var ball in balls)
            {
                CheckForCollision(ball);
                

            }
            
            Invalidate(); // Refresh the form

        }
        /// <summary>
        /// The delgate which decide if any ball is to be removed
        /// </summary>
        /// <param name="ball"></param>
        /// <returns></returns>
        private bool BallMissesPaddle(Ball ball) // if ball goes below bottom of form
        {
            return (ball.currentY >= DisplayRectangle.Bottom);
        }



        //private bool BallHitsBox(Ball ball)
        //{
        //    if (box.DisplayRect.IntersectsWith(ball.DisplayArea))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// The Collision Detection Method
        /// </summary>

        private void CheckForCollision(Ball ball)
        {
            // Check the Top side collision Detection 
            if (ball.currentY <= this.DisplayRectangle.Top)
            {
                ball.FlipY();
            }
            // Check the paddle collision Detection 
            else if (paddle.DisplayRect.IntersectsWith(ball.DisplayArea))
            {
                ball.FlipY();
            }
            foreach(var box in boxes)
            {
                if (box.DisplayRect.IntersectsWith(ball.DisplayArea)) // if ball hits box
                {
                    ball.FlipY();
                    BoxCollisionCounter++; // add to counter when box is it
                    
                    box.MoveBox(); //  move box out of screen aka removes box
                    
                 
                    
                }
            }
            //else if (box.DisplayRect.IntersectsWith(ball.DisplayArea))
            //{
                

            //}
            //To decrease the value of number of balls on the top of the main form of our game
            //else if (ball.currentY >= DisplayRectangle.Bottom)
            //{
            //    balls.Remove(ball);
            //}

            // Check the Left side collision Detection
            if (ball.currentX <= this.DisplayRectangle.Left)
            {
                ball.FlipX();
            }
            // Check the right collision Detection
            else if (ball.currentX >= (this.DisplayRectangle.Right-ball.Size))
            {
                ball.FlipX();
            }

            //Windows GDI+ is a class-based API for C/C++ programmers. It enables applications to use graphics and
            //formatted text on both the video display and the printer. Applications based on the Microsoft Win32 API do not
            //access graphics hardware directly. Instead, GDI+ interacts with device drivers on behalf of applications.
            //In computer graphics, double buffering is a technique for drawing graphics that shows no (or less) stutter, tearing, and other artifacts.
            //It is difficult for a program to draw a display so that pixels do not change more than once.
        }
    }
}
