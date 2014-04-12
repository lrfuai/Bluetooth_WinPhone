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

    bool m_Customer = false;
    public const double BallRadius = 40;

    public const double Reibung = 0.8;
    double ancho;
    double altura;

    public void Game(double width, double height)
    {
        ancho = width;
        altura = height;

        Ball = new Point(ancho / 2d, altura / 2d);
        Vektor = new Point(100, 100);
    }

    public void Updated(long ms, double width, double height)
    {
        ancho = width;
        altura = height;
        double speed = ms / 1000d;

        //Consulta cuando el usuario maneja la bola
        if (Customer == false)
        {
            Vektor = new Point(Vektor.X - (Vektor.X * Reibung * speed), Vektor.Y - (Vektor.Y * Reibung * speed));
            Ball = new Point(Ball.X + (Vektor.X * speed), Ball.Y + (Vektor.Y * speed));
            Vektor = new Point(Math.Round((Vektor.X + (Force.X * speed * 100d)) - 0.05d, 1), Math.Round((Vektor.Y + (Force.Y * speed * 100d)) - 0.05d, 1));
            //Uso un Point porque tengo dos direcciones, X e Y
            //Force es la gravedad (Desde el sensor del acelerómetro). 
            //Reibung es la pérdida por fricción. Lo necesitamos porque la bola va más lenta. 

            //Si la posición de la bola llega al borde, la tiro para atrás
            if (Ball.X < BallRadius)
            {
                Ball = new Point(BallRadius, Ball.Y);
                Vektor = new Point(Math.Round(-Vektor.X * 0.9d, 1), Vektor.Y);
                //El mismo procedimiento para el resto.
            }
            else if (Ball.X > ancho - BallRadius)
            {
                Ball = new Point(ancho - BallRadius, Ball.Y);
                Vektor = new Point(Math.Round(-Vektor.X * 0.9d, 1), Vektor.Y);
            }
            else if (Ball.Y < BallRadius)
            {
                Ball = new Point(Ball.X, BallRadius);
                Vektor = new Point(Vektor.X, Math.Round(-Vektor.Y * 0.9, 1));
            }
            else if (Ball.Y > altura - BallRadius)
            {
                Ball = new Point(Ball.X, altura - BallRadius);
                Vektor = new Point(Vektor.X, Math.Round(-Vektor.Y * 0.9d, 1));
            }
        }

    }

    public Point Ball
    {
        
        get { return m_Ball; }
        
        set { m_Ball = value; }
    }

    public Point Vektor
    {
        
        get { return m_Vektor; }
        
        set { m_Vektor = value; }
    }

    //Para saber si lo esta manipulando el user
    public bool Customer
    {
        
        get { return m_Customer; }
        
        set { m_Customer = value; }
    }

    public Point Force
    {
        
        get { return m_Force; }
        
        set { m_Force = value; }
    }
}