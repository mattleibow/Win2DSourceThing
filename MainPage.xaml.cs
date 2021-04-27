// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace SimpleSample
{
	/// <summary>
	/// Draws some graphics using Win2D
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();

			imageControl.Loaded += MainPage_Loaded;
		}

		private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			imageControl.XamlRoot.Changed += XamlRoot_Changed;
		}

		private void XamlRoot_Changed(Windows.UI.Xaml.XamlRoot sender, Windows.UI.Xaml.XamlRootChangedEventArgs args)
		{
			imageControl.Source = RenderImageSource((float)sender.RasterizationScale);
		}

		internal CanvasImageSource RenderImageSource(float scale)
		{
			var dpi = scale * 96;

			var fontFamily = "ms-appx:///Assets/ionicons.ttf#Ionicons";
			var fontSize = 30;
			var color = Colors.White;

			var textFormat = new CanvasTextFormat
			{
				FontFamily = fontFamily,
				FontSize = fontSize,
				HorizontalAlignment = CanvasHorizontalAlignment.Center,
				VerticalAlignment = CanvasVerticalAlignment.Center,
				Options = CanvasDrawTextOptions.Default
			};

			var device = CanvasDevice.GetSharedDevice();
			var layout = new CanvasTextLayout(device, "\uf2fe", textFormat, fontSize, fontSize);

			// add a 1px padding all around
			var canvasWidth = (float)layout.LayoutBounds.Width + 2;
			var canvasHeight = (float)layout.LayoutBounds.Height + 2;

			var canvasImageSource = new CanvasImageSource(device, canvasWidth, canvasHeight, dpi);
			using (var ds = canvasImageSource.CreateDrawingSession(Colors.Transparent))
			{
				// offset by 1px as we added a 1px padding
				var x = (float)layout.DrawBounds.X * -1;

				ds.DrawTextLayout(layout, x, 1f, color);
			}

			return canvasImageSource;
		}

		void canvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
		{
			var textFormat = new CanvasTextFormat
			{
				FontFamily = "ms-appx:///Assets/ionicons.ttf#Ionicons",
				FontSize = 30,
				HorizontalAlignment = CanvasHorizontalAlignment.Center,
				VerticalAlignment = CanvasVerticalAlignment.Center,
				Options = CanvasDrawTextOptions.Default
			};

			var layout = new CanvasTextLayout(sender.Device, "\uf2fe", textFormat, 30, 30);

			args.DrawingSession.DrawTextLayout(layout, 23, 23, Colors.White);
		}
	}
}
