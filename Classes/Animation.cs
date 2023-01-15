using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuranLai.Algorithms
{
    public interface IAnimation
    {
        void StartAnimationAsync();
    }

    public class Animation : IAnimation
    {
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        static int offset;
        static int duration;
        static double start, end;
        static Func<double, Animation, double> MappingFunction;
        static Action<double> ApplyValue;
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

        /// <summary>
        /// Initialize the Animation class.
        /// </summary>
        /// <param name="Duration">The time length of the animation.</param>
        /// <param name="Start">The value of object at the start point.</param>
        /// <param name="End">The value of object at the end point.</param>
        /// <param name="mappingFunction">The function to calculate <y,x> mapping.</param>
        /// <param name="applyValue">The Action to apply results.</param>
        /// <param name="Offset">The variable controls ease options</param>
        public Animation
           (int Duration,
            double Start,
            double End,
            Func<double, Animation, double> mappingFunction,
            Action<double> applyValue,
            int Offset = 0)
        {
            offset = Offset;
            duration = Duration;
            start = Start;
            end = End;
            MappingFunction = mappingFunction;
            ApplyValue = applyValue;
        }

        /// <summary>
        /// Start the animation manually.
        /// </summary>
        /// <param name="ApplyValue">Some actions you want to do after calculating the variable.</param>
        public async void StartAnimationAsync()
        {
            DateTime now = DateTime.Now;
            TimeSpan span = TimeSpan.Zero;
            while (span.TotalMilliseconds <= (offset + duration))
            {
                await Task.Run(() =>
                {
                    double time = span.TotalMilliseconds;
                    double value = MappingFunction(time, this);
                    ApplyValue(value);
                });
                span = DateTime.Now - now;
            }
            ApplyValue(end);
        }

        /// <summary>
        /// Get the result value by Math.Sin() function.
        /// </summary>
        /// <param name="span">Time spent in StartAnimation Function.</param>
        /// <returns>The result value.</returns>
        public static double GetSineValue(double span, Animation animation)
        {
            double x_axis = Math.PI * span / duration / 2;
            double final = Math.PI * (offset + duration) / duration / 2;
            double k = (end - start) / Math.Sin(final);
            double value = start + Math.Sin(x_axis) * k;

            return value;
        }

        /// <summary>
        /// Get the result value by linear function.
        /// </summary>
        /// <param name="span">Time spent in StartAnimation Function.</param>
        /// <returns>The result value.</returns>
        public static double GetLinearValue(double span, Animation animation)
        {
            double speed = (end - start) / duration;
            double value = start + span * speed;
            return value;
        }
    }

    public class AnimationPool
    {
        private static List<Animation> animations = new();

        public AnimationPool()
        {
            animations = new();
        }

        public void Add
           (int Duration,
            double Start,
            double End,
            Func<double, Animation, double> mappingFunction,
            Action<double> applyValue,
            int Offset = 0)
        {
            Animation animation = new Animation(Duration, Start, End, mappingFunction, applyValue, Offset);
            animations.Add(animation);
        }

        public void StartAllAnimations()
        {
            foreach (Animation animation in animations)
            {
                Task.Run(animation.StartAnimationAsync);
            }
        }
    }
}
