﻿using System;
using UIKit;
using RectangleF = CoreGraphics.CGRect;

namespace Xamarin.Platform.Handlers
{
	public partial class SwitchHandler : AbstractViewHandler<ISwitch, UISwitch>
	{
		static UIColor? DefaultOnColor;
		static UIColor? DefaultThumbColor;

		protected override UISwitch CreateNativeView()
		{
			return new UISwitch(RectangleF.Empty);
		}

		protected override void ConnectHandler(UISwitch nativeView)
		{
			nativeView.ValueChanged += OnControlValueChanged;
		}

		protected override void DisconnectHandler(UISwitch nativeView)
		{
			nativeView.ValueChanged -= OnControlValueChanged;
		}

		protected override void SetupDefaults(UISwitch nativeView)
		{
			DefaultOnColor = UISwitch.Appearance.OnTintColor;
			DefaultThumbColor = UISwitch.Appearance.ThumbTintColor;
		}

		public static void MapIsToggled(SwitchHandler handler, ISwitch view)
		{
			ViewHandler.CheckParameters(handler, view);
			handler.TypedNativeView?.UpdateIsToggled(view);
		}

		public static void MapOnColor(SwitchHandler handler, ISwitch view)
		{
			ViewHandler.CheckParameters(handler, view);
			handler.TypedNativeView?.UpdateOnColor(view, DefaultOnColor);
		}

		public static void MapThumbColor(SwitchHandler handler, ISwitch view)
		{
			ViewHandler.CheckParameters(handler, view);
			handler.TypedNativeView?.UpdateThumbColor(view, DefaultThumbColor);
		}

		void OnControlValueChanged(object? sender, EventArgs e)
		{
			if (VirtualView == null)
				return;

			if (TypedNativeView != null)
				VirtualView.IsToggled = TypedNativeView.On;

			VirtualView.Toggled();
		}
	}
}