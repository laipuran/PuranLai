using System.Diagnostics;

namespace PuranLai.Algorithms
{
    public interface IAnimation
    {
        void StartAnimationAsync();
    }

    public class Animation : IAnimation
    {
        public int offset;
        public int duration;
        public double start, end;
        public Func<double, Animation, double> MappingFunction;
        public Action<double> ApplyValue;
        public unsafe bool* flag;

        /// <summary>
        /// Initialize the Animation class.
        /// </summary>
        /// <param name="Duration">The time length of the animation.</param>
        /// <param name="Start">The value of object at the start point.</param>
        /// <param name="End">The value of object at the end point.</param>
        /// <param name="mappingFunction">The function to calculate <y,x> mapping.</param>
        /// <param name="applyValue">The Action to apply results.</param>
        /// <param name="Offset">The variable controls ease options.</param>
        /// <param name="Flag">The Boolean pointer to stop the animation.</param>
        public unsafe Animation
           (int Duration,
            double Start,
            double End,
            Func<double, Animation, double> mappingFunction,
            Action<double> applyValue,
            int Offset = 0,
            bool* Flag = null)
        {
            duration = Duration;
            start = Start;
            end = End;
            MappingFunction = mappingFunction;
            ApplyValue = applyValue;
            offset = Offset;
            flag = Flag;
        }

        /// <summary>
        /// Start the animation manually.
        /// </summary>
        public async void StartAnimationAsync()
        {
            DateTime now = DateTime.Now;
            TimeSpan span = TimeSpan.Zero;
            bool status = true;
            unsafe
            {
                if (flag is not null)
                {
                    status = *flag;
                }
            }
            while (span.TotalMilliseconds <= (offset + duration))
            {
                unsafe
                {
                    if (flag is not null)
                    {
                        if (*flag != status)
                            break;
                    }
                }
                span = DateTime.Now - now;
                await Task.Run(() =>
                {
                    unsafe
                    {
                        double time = span.TotalMilliseconds;
                        double value = this.MappingFunction(time, this);
                        if (time > (offset + duration))
                            return;
                        if (flag is not null)
                        {
                            if (*flag != status)
                                return;
                        }
                        // Debug.WriteLine(time + " " + value);
                        ApplyValue(value);
                        return;
                    }
                });
            }
        }

        /// <summary>
        /// Get the result value by Math.Sin() function.
        /// </summary>
        /// <param name="span">Time spent in StartAnimation Function.</param>
        /// <returns>The result value.</returns>
        public static double GetSineValue(double span, Animation animation)
        {
            double x_axis = Math.PI * span / animation.duration / 2;
            double final = Math.PI * (animation.duration + animation.offset) / animation.duration / 2;
            double k = (animation.end - animation.start) / Math.Sin(final);
            double value = animation.start + Math.Sin(x_axis) * k;

            return value;
        }

        /// <summary>
        /// Get the result value by linear function.
        /// </summary>
        /// <param name="span">Time spent in StartAnimation Function.</param>
        /// <returns>The result value.</returns>
        public static double GetLinearValue(double span, Animation animation)
        {
            double speed = (animation.end - animation.start) / animation.duration;
            double value = animation.start + span * speed;
            return value;
        }
    }

    public interface IAnimationPool
    {
        void Add
           (int Duration,
            double Start,
            double End,
            Func<double, Animation, double> mappingFunction,
            Action<double> applyValue,
            int Offset = 0);
        void StartAllAnimations();
    }

    public class AnimationPool : IAnimationPool
    {
        public List<Animation> animations = new();

        public AnimationPool()
        {
            animations = new();
        }

        /// <summary>
        /// Add a new Animation (up to 3) to the AnimationPool
        /// </summary>
        public unsafe void Add
           (int Duration,
            double Start,
            double End,
            Func<double, Animation, double> mappingFunction,
            Action<double> applyValue,
            int Offset = 0)
        {
            animations.Add(new(Duration, Start, End, mappingFunction, applyValue, Offset));
        }

        /// <summary>
        /// Start all the animations in the AnimationPool
        /// </summary>
        public async void StartAllAnimations()
        {
            foreach (Animation animation in this.animations)
            {
                await Task.Run(animation.StartAnimationAsync);
            }
        }
    }
}
