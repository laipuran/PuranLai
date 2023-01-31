using PuranLai.Algorithms;
using System;
using System.Windows;
using System.Windows.Media;

namespace PuranLai.Tools
{
    public static class ExtendedWindowOps
    {
        public enum OpacityOptions
        {
            _0 = 0,
            _1 = 1,
            _75 = 192,
            _25 = 64,
        }

        public static unsafe void ChangeOpacity(this Window window, OpacityOptions end, bool* isMouseIn = null)
        {
            if (isMouseIn is not null)
            {
                switch (end == OpacityOptions._75)
                {
                    case true:
                        {
                            *isMouseIn = true;
                            break;
                        }
                    case false:
                        {
                            *isMouseIn = false;
                            break;
                        }
                }
            }

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
            Animation white = new(500, color.A, (double)end, Animation.GetLinearValue, SetAlpha, Flag: isMouseIn);
            white.StartAnimationAsync();
        }

        public static unsafe void ChangeOpacity(this Window window, int end, bool* isMouseIn = null)
        {
            if (isMouseIn is not null)
            {
                switch (end >= 128)
                {
                    case true:
                        {
                            *isMouseIn = true;
                            break;
                        }
                    case false:
                        {
                            *isMouseIn = false;
                            break;
                        }
                }
            }

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
            Animation white = new(500, color.A, end, Animation.GetLinearValue, SetAlpha, Flag: isMouseIn);
            white.StartAnimationAsync();
        }
    }
}
