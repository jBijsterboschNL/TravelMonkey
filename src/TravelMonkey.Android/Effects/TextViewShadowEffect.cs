using Android.Graphics;
using Android.Widget;
using System;
using System.Linq;
using TravelMonkey.Droid.Effects;
using TravelMonkey.Effects;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportEffect(typeof(TextViewShadowEffect), nameof(ShadowEffect))]
namespace TravelMonkey.Droid.Effects
{
	public class TextViewShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
				if (effect == null) return;

				float radius = effect.Radius;
				float distanceX = effect.DistanceX;
				float distanceY = effect.DistanceY;
				Color color = effect.Color.ToAndroid();

				if (Control is TextView textView)
				{
					textView.SetShadowLayer(radius, distanceX, distanceY, color);
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