using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
public class game
{
    Point m_Ball;
    Point m_Vektor;
    Point m_Force;
    // Modulweit gültige Variable /Module-Wide variable
    bool m_Customer = false;
    public const double BallRadius = 40;
    //der Faktor um den der Ball langsamer wird/the factor that get the ball slower
    public const double Reibung = 0.8;
    double ancho;

    double altura;
    public void Game(double width, double height)
    {
        ancho = width;
        altura = height;

        Ball = new Point(ancho / 2, altura / 2);
        Vektor = new Point(100, 100);
    }
    /// <summary>
    /// erneuert die Positionsdaten des Balls/renewing the position of the ball
    /// (ms is milliseconds, width is the width of the display, height is the height of the display)
    /// </summary>
    /// <remarks></remarks>
    public void Updated(long ms, double width, double height)
    {
        ancho = width;
        altura = height;
        double speed = ms / 1000;

        //Frage ob der Benutzer grad seine Hande im Spiel hat/asks whether the player handle the ball
        if (Customer == false)
        {
            Vektor = new Point(Vektor.X - (Vektor.X * Reibung * speed), Vektor.Y - (Vektor.Y * Reibung * speed));
            Ball = new Point(Ball.X + (Vektor.X * speed), Ball.Y + (Vektor.Y * speed));
            Vektor = new Point(Math.Round((Vektor.X + (Force.X * speed * 100)) - 0.05, 1), Math.Round((Vektor.Y + (Force.Y * speed * 100)) - 0.05, 1));
            //I used Point because I have 2 direction X and Y
            //force is the gravity (from the acceleration sensor)
            //Reibung is the friction loss. We need it because the ball to be slower

            //Wenn die Ballpositon am Rande des Displayes ist dann soll er zurück geworfen werden/if the ballposition is on the edge of the display the ball thrown back
            if (Ball.X < BallRadius)
            {
                Ball = new Point(BallRadius, Ball.Y);
                Vektor = new Point(Math.Round(-Vektor.X * 0.9, 1), Vektor.Y);
                //das selbe jetzt nochmal für alle anderen Seiten/the same procedure for the remaining sites
            }
            else if (Ball.X > ancho - BallRadius)
            {
                Ball = new Point(ancho - BallRadius, Ball.Y);
                Vektor = new Point(Math.Round(-Vektor.X * 0.9, 1), Vektor.Y);
            }
            else if (Ball.Y < BallRadius)
            {
                Ball = new Point(Ball.X, BallRadius);
                Vektor = new Point(Vektor.X, Math.Round(-Vektor.Y * 0.9, 1));
            }
            else if (Ball.Y > altura - BallRadius)
            {
                Ball = new Point(Ball.X, altura - BallRadius);
                Vektor = new Point(Vektor.X, Math.Round(-Vektor.Y * 0.9, 1));
            }
        }

    }

    public Point Ball
    {
        // Abholen des Eigenschaftenwerts 
        get { return m_Ball; }
        // Setzen des Eigenschaftenwerts 
        set { m_Ball = value; }
    }

    public Point Vektor
    {
        // Abholen des Eigenschaftenwerts 
        get { return m_Vektor; }
        // Setzen des Eigenschaftenwerts 
        set { m_Vektor = value; }
    }

    public bool Customer
    {
        // Abholen des Eigenschaftenwerts 
        get { return m_Customer; }
        // Setzen des Eigenschaftenwerts 
        set { m_Customer = value; }
    }

    public Point Force
    {
        // Abholen des Eigenschaftenwerts 
        get { return m_Force; }
        // Setzen des Eigenschaftenwerts 
        set { m_Force = value; }
    }
}