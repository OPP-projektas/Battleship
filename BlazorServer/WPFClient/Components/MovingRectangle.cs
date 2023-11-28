using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WPFClient.Components
{
    public class MovingRectangle : UserControl
    {
        public Rectangle rectangle;
        public Brush fill { get; set; } = null;
        public MovingRectangle(Brush color)
        {
            rectangle = new Rectangle
            {
                Width = 10,
                Height = 10,
                Fill = color
            };

            Content = rectangle;
        }

    }
}