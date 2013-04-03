using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MyWmp.UIElements
{
	public class FormatedSlider : Slider
	{
		private ToolTip autoToolTip_;

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

		private void FormatAutoToolTipContent()
		{
		    if (this.AutoToolTipFormat == "time")
		        this.AutoToolTip.Content = TimeSpan.FromSeconds(double.Parse((string) this.AutoToolTip.Content)).ToString();
		    else
		        this.AutoToolTip.Content = (int) (double.Parse((string) this.AutoToolTip.Content) * 100);
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