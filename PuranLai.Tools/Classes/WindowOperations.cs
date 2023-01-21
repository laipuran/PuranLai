using PuranLai.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace PuranLai.Tools.Classes
{
    public class WindowOperations
    {
        public static unsafe void ToWhite(bool* isMouseIn, Window window)
        {
            *isMouseIn = true;
            Color color = ((SolidColorBrush)window.Background).Color;
            Action<double> SetAlpha = new((double value) =>
            {
                double alpha = value;
                Action<double> set = new((double value) =>
                {
                    color.A = (byte)alpha;
                    window.Background = new SolidColorBrush(color);
                });
                window.Dispatcher.Invoke(set, alpha);
            });
            Animation white = new Animation(500, color.A, 128 + 64, Animation.GetLinearValue, SetAlpha, Flag: isMouseIn);
            white.StartAnimationAsync();
        }
        public static unsafe void ToTransparent(bool* isMouseIn, Window window)
        {
            *isMouseIn = true;
            Color color = ((SolidColorBrush)window.Background).Color;
            Action<double> SetAlpha = new((double value) =>
            {
                double alpha = value;
                Action<double> set = new((double value) =>
                {
                    color.A = (byte)alpha;
                    window.Background = new SolidColorBrush(color);
                });
                window.Dispatcher.Invoke(set, alpha);
            });
            Animation white = new Animation(500, color.A, 64, Animation.GetLinearValue, SetAlpha, Flag: isMouseIn);
            white.StartAnimationAsync();
        }
    }
}
