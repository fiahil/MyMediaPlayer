using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MyWmp.UIElements
{
	public class FormatedSlider : Slider
	{
		private ToolTip autoToolTip_;

        public FormatedSlider()
        {
            this.FormatedValue = "50";
        }

	    public string AutoToolTipFormat { get; set; }

	    protected override void OnThumbDragStarted(DragStartedEventArgs e)
		{
			base.OnThumbDragStarted(e);
			this.FormatAutoToolTipContent();
		}

		protected override void OnThumbDragDelta(DragDeltaEventArgs e)
		{
			base.OnThumbDragDelta(e);
			this.FormatAutoToolTipContent();
		}

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            if (this.AutoToolTipFormat == "volume")
                this.FormatedValue = ((int)(this.Value * 100)).ToString();
        }

		private void FormatAutoToolTipContent()
		{
		    if (this.AutoToolTipFormat == "time")
		        this.AutoToolTip.Content = TimeSpan.FromSeconds(double.Parse((string) this.AutoToolTip.Content)).ToString();
		    else
		    {
		        var v = (int) (double.Parse((string) this.AutoToolTip.Content) * 100);
		        this.FormatedValue = v.ToString();
		        this.AutoToolTip.Content = v;
		    }
		}

	    public static readonly DependencyProperty FormatedValueProperty =
	        DependencyProperty.Register("FormatedValue", typeof (string), typeof (FormatedSlider), new PropertyMetadata(default(string)));

	    public string FormatedValue
	    {
	        get { return (string) GetValue(FormatedValueProperty); }
	        set { SetValue(FormatedValueProperty, value); }
	    }

		private ToolTip AutoToolTip
		{
			get
			{
				if (autoToolTip_ == null)
				{
					var field = typeof(Slider).GetField(
						"_autoToolTip",
						BindingFlags.NonPublic | BindingFlags.Instance);

				    autoToolTip_ = (ToolTip) field.GetValue(this);
				}

				return autoToolTip_;
			}
		}	
	}
}