using CoreGraphics;
using System;
using System.Linq;
using TravelMonkey.Effects;
using TravelMonkey.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(UILabelShadowEffect), nameof(ShadowEffect))]
namespace TravelMonkey.iOS.Effects
{
	public class UILabelShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
				if (effect == null) return;

				if (Control is UILabel label)
				{
					label.Layer.CornerRadius = effect.Radius;
					label.Layer.ShadowColor = effect.Color.ToCGColor();
					label.Layer.ShadowOffset = new CGSize(effect.DistanceX, effect.DistanceY);
					label.Layer.ShadowOpacity = 1.0f;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}

		protected override void OnDetached()
		{
		}
	}
}