using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuranLai.Algorithms
{
    public interface IAnimation
    {
        void StartAnimationAsync(Action<double> action);
        double GetSineValue(double span);
        double GetLinearValue(double span);
    }
    public class Animation : IAnimation
    {
        int offset;
        int duration;
        double start, end;
        Func<double, double> MappingFunction;
        unsafe double* variable;

        /// <summary>
        /// Initialize the Animation class.
        /// </summary>
        /// <param name="offset">The variable controls ease options</param>
        /// <param name="duration">The time length of the animation.</param>
        /// <param name="start">The value of object at the start point.</param>
        /// <param name="end">The value of object at the end point.</param>
        /// <param name="mappingFunction">The function to calculate <y,x> mapping.</param>
        /// <param name="variable">The pointer pointing to the destination variable</param>
        public unsafe Animation
           (int offset,
            int duration,
            double start,
            double end,
            Func<double, double> mappingFunction,
            double* variable = null)
        {
            this.offset = offset;
            this.duration = duration;
            this.start = start;
            this.end = end;
            MappingFunction = mappingFunction;
            this.variable = variable;
        }

        /// <summary>
        /// Start the animation manually.
        /// </summary>
        /// <param name="ApplyValue">Some actions you want to do after calculating the variable.</param>
        public async void StartAnimationAsync(Action<double>? ApplyValue = null)
        {
            DateTime now = DateTime.Now;
            TimeSpan span = TimeSpan.Zero;
            while (span.TotalMicroseconds <= duration)
            {
                await Task.Run(() => 
                {
                    try
                    {
                        unsafe
                        {
                            double time = span.TotalMilliseconds;
                            *variable = MappingFunction(time);
                            if (ApplyValue is null)
                                return;
                            ApplyValue(time);
                        }
                    }
                    catch { }
                });
                span = DateTime.Now - now;
            }
        }

        /// <summary>
        /// Get the result value by Sine function.
        /// </summary>
        /// <param name="span">Time spent in StartAnimation Function.</param>
        /// <returns>The result value.</returns>
        public double GetSineValue(double span)
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
        public double GetLinearValue(double span)
        {
            double speed = (end - start) / duration;
            double value = start + span * speed;
            return value;
        }
    }
}
