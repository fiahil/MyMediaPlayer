using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Timer = System.Timers.Timer;

namespace MyWmp.Behaviors
{
    public class MediaTimeSlider : Behavior<MediaElement>
    {
        private readonly Timer timer_;

        public MediaTimeSlider()
        {
            this.timer_ = new Timer(250)
                {
                    AutoReset = true
                };
            this.timer_.Elapsed += (sender, args) => Dispatcher.Invoke(() => this.Position = this.AssociatedObject.Position.TotalSeconds);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.Maximum = 1;
            this.Position = 0;
            this.AssociatedObject.MediaOpened += (sender, args) => this.Maximum = this.AssociatedObject.NaturalDuration.TimeSpan.TotalSeconds;
            this.timer_.Start();
        }

        protected override void OnDetaching()
        {
            this.timer_.Stop();
            base.OnDetaching();
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof (double), typeof (MediaTimeSlider), new PropertyMetadata(default(double)));
        public double Maximum
        {
            get { return (double) GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(double), typeof(MediaTimeSlider), new PropertyMetadata((o, args) =>
                {
                    ((MediaTimeSlider) o).Position = (double) args.NewValue;
                }));
        public double Position
        {
            get { return (double) GetValue(PositionProperty); }
            set
            {
                SetValue(PositionProperty, value);
                this.AssociatedObject.Position = TimeSpan.FromSeconds(value);
            }
        }
    }
}
