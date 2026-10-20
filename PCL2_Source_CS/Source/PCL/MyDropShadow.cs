// PCL

namespace PCL
{
	public class MyDropShadow : Decorator
	{
		private enum Placement
		{
			TopLeft,
			Top,
			TopRight,
			Left,
			Center,
			Right,
			BottomLeft,
			Bottom,
			BottomRight
		}
		public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(System.Windows.Media.Color), typeof(MyDropShadow), new FrameworkPropertyMetadata(System.Windows.Media.Color.FromArgb(113, 0, 0, 0), FrameworkPropertyMetadataOptions.AffectsRender, ClearBrushes));
		public static readonly DependencyProperty ShadowRadiusProperty = DependencyProperty.Register("ShadowRadius", typeof(double), typeof(MyDropShadow), new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsRender, ClearBrushes));
		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MyDropShadow), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.AffectsRender, ClearBrushes), IsCornerRadiusValid);
		private static System.Windows.Media.Brush[] _commonBrushes;
		private static CornerRadius _commonCornerRadius;
		private static readonly object _resourceAccess = RuntimeHelpers.GetObjectValue(new object());
		private System.Windows.Media.Brush[] _brushes;
		public System.Windows.Media.Color Color
		{
			get
			{
				object value = GetValue(ColorProperty);
				if (value == null)
				{
					return default;
				}
				return (System.Windows.Media.Color)value;
			}
			set
			{
				SetValue(ColorProperty, value);
			}
		}
		public double ShadowRadius
		{
			get
			{
				return Conversions.ToDouble(GetValue(ShadowRadiusProperty));
			}
			set
			{
				SetValue(ShadowRadiusProperty, value);
			}
		}
		public CornerRadius CornerRadius
		{
			get
			{
				object value = GetValue(CornerRadiusProperty);
				if (value == null)
				{
					return default;
				}
				return (CornerRadius)value;
			}
			set
			{
				SetValue(CornerRadiusProperty, value);
			}
		}
		private static bool IsCornerRadiusValid(object value)
		{
			CornerRadius cornerRadius = ((value != null) ? ((CornerRadius)value) : default(CornerRadius));
			if (!(cornerRadius.TopLeft < 0.0) && !(cornerRadius.TopRight < 0.0) && !(cornerRadius.BottomLeft < 0.0) && !(cornerRadius.BottomRight < 0.0) && !double.IsNaN(cornerRadius.TopLeft) && !double.IsNaN(cornerRadius.TopRight) && !double.IsNaN(cornerRadius.BottomLeft) && !double.IsNaN(cornerRadius.BottomRight) && !double.IsInfinity(cornerRadius.TopLeft) && !double.IsInfinity(cornerRadius.TopRight) && !double.IsInfinity(cornerRadius.BottomLeft))
			{
				return !double.IsInfinity(cornerRadius.BottomRight);
			}
			return false;
		}
		protected override void OnRender(DrawingContext drawingContext)
		{
			CornerRadius cornerRadius = CornerRadius;
			Rect rect = new Rect(0.0, 0.0, RenderSize.Width, RenderSize.Height);
			System.Windows.Media.Color color = Color;
			if (!(rect.Width > 0.0) || !(rect.Height > 0.0) || color.A <= 0)
			{
				return;
			}
			double num = rect.Right - rect.Left - 2.0 * ShadowRadius;
			double num2 = rect.Bottom - rect.Top - 2.0 * ShadowRadius;
			double val = Math.Min(num * 0.5, num2 * 0.5);
			cornerRadius.TopLeft = Math.Min(cornerRadius.TopLeft, val);
			cornerRadius.TopRight = Math.Min(cornerRadius.TopRight, val);
			cornerRadius.BottomLeft = Math.Min(cornerRadius.BottomLeft, val);
			cornerRadius.BottomRight = Math.Min(cornerRadius.BottomRight, val);
			System.Windows.Media.Brush[] brushes = GetBrushes(color, cornerRadius);
			double num3 = rect.Top + ShadowRadius;
			double num4 = rect.Left + ShadowRadius;
			double num5 = rect.Right - ShadowRadius;
			double num6 = rect.Bottom - ShadowRadius;
			double[] array = new double[6]
			{
				num4,
				num4 + cornerRadius.TopLeft,
				num5 - cornerRadius.TopRight,
				num4 + cornerRadius.BottomLeft,
				num5 - cornerRadius.BottomRight,
				num5
			};
			double[] array2 = new double[6]
			{
				num3,
				num3 + cornerRadius.TopLeft,
				num3 + cornerRadius.TopRight,
				num6 - cornerRadius.BottomLeft,
				num6 - cornerRadius.BottomRight,
				num6
			};
			drawingContext.PushGuidelineSet(new GuidelineSet(array, array2));
			cornerRadius.TopLeft += ShadowRadius;
			cornerRadius.TopRight += ShadowRadius;
			cornerRadius.BottomLeft += ShadowRadius;
			cornerRadius.BottomRight += ShadowRadius;
			drawingContext.DrawRectangle(rectangle: new Rect(rect.Left, rect.Top, cornerRadius.TopLeft, cornerRadius.TopLeft), brush: brushes[0], pen: null);
			double num7 = array[2] - array[1];
			if (num7 > 0.0)
			{
				drawingContext.DrawRectangle(rectangle: new Rect(array[1], rect.Top, num7, ShadowRadius), brush: brushes[1], pen: null);
			}
			drawingContext.DrawRectangle(rectangle: new Rect(array[2], rect.Top, cornerRadius.TopRight, cornerRadius.TopRight), brush: brushes[2], pen: null);
			double num8 = array2[3] - array2[1];
			if (num8 > 0.0)
			{
				drawingContext.DrawRectangle(rectangle: new Rect(rect.Left, array2[1], ShadowRadius, num8), brush: brushes[3], pen: null);
			}
			double num9 = array2[4] - array2[2];
			if (num9 > 0.0)
			{
				drawingContext.DrawRectangle(rectangle: new Rect(array[5], array2[2], ShadowRadius, num9), brush: brushes[5], pen: null);
			}
			drawingContext.DrawRectangle(rectangle: new Rect(rect.Left, array2[3], cornerRadius.BottomLeft, cornerRadius.BottomLeft), brush: brushes[6], pen: null);
			double num10 = array[4] - array[3];
			if (num10 > 0.0)
			{
				drawingContext.DrawRectangle(rectangle: new Rect(array[3], array2[5], num10, ShadowRadius), brush: brushes[7], pen: null);
			}
			drawingContext.DrawRectangle(rectangle: new Rect(array[4], array2[4], cornerRadius.BottomRight, cornerRadius.BottomRight), brush: brushes[8], pen: null);
			if (cornerRadius.TopLeft == ShadowRadius && cornerRadius.TopLeft == cornerRadius.TopRight && cornerRadius.TopLeft == cornerRadius.BottomLeft && cornerRadius.TopLeft == cornerRadius.BottomRight)
			{
				drawingContext.DrawRectangle(rectangle: new Rect(array[0], array2[0], num, num2), brush: brushes[4], pen: null);
			}
			else
			{
				PathFigure pathFigure = new PathFigure();
				if (cornerRadius.TopLeft > ShadowRadius)
				{
					pathFigure.StartPoint = new System.Windows.Point(array[1], array2[0]);
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[1], array2[1]), isStroked: true));
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[0], array2[1]), isStroked: true));
				}
				else
				{
					pathFigure.StartPoint = new System.Windows.Point(array[0], array2[0]);
				}
				if (cornerRadius.BottomLeft > ShadowRadius)
				{
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[0], array2[3]), isStroked: true));
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[3], array2[3]), isStroked: true));
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[3], array2[5]), isStroked: true));
				}
				else
				{
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[0], array2[5]), isStroked: true));
				}
				if (cornerRadius.BottomRight > ShadowRadius)
				{
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[4], array2[5]), isStroked: true));
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[4], array2[4]), isStroked: true));
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[5], array2[4]), isStroked: true));
				}
				else
				{
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[5], array2[5]), isStroked: true));
				}
				if (cornerRadius.TopRight > ShadowRadius)
				{
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[5], array2[2]), isStroked: true));
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[2], array2[2]), isStroked: true));
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[2], array2[0]), isStroked: true));
				}
				else
				{
					pathFigure.Segments.Add(new LineSegment(new System.Windows.Point(array[5], array2[0]), isStroked: true));
				}
				pathFigure.IsClosed = true;
				pathFigure.Freeze();
				PathGeometry pathGeometry = new PathGeometry();
				pathGeometry.Figures.Add(pathFigure);
				pathGeometry.Freeze();
				drawingContext.DrawGeometry(brushes[4], null, pathGeometry);
			}
			drawingContext.Pop();
		}
		private static void ClearBrushes(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			((MyDropShadow)o)._brushes = null;
		}
		private GradientStopCollection CreateStops(System.Windows.Media.Color c, double cornerRadius)
		{
			double num = 1.0 / (ShadowRadius + cornerRadius);
			GradientStopCollection gradientStopCollection = new GradientStopCollection();
			System.Windows.Media.Color color = c;
			gradientStopCollection.Add(new GradientStop(color, (ShadowRadius * 0.1 + cornerRadius) * num));
			checked
			{
				color.A = (byte)Math.Round(0.74336 * (double)unchecked((int)c.A));
				gradientStopCollection.Add(new GradientStop(color, (ShadowRadius * 0.3 + cornerRadius) * num));
				color.A = (byte)Math.Round(0.38053 * (double)unchecked((int)c.A));
				gradientStopCollection.Add(new GradientStop(color, (ShadowRadius * 0.5 + cornerRadius) * num));
				color.A = (byte)Math.Round(0.12389 * (double)unchecked((int)c.A));
				gradientStopCollection.Add(new GradientStop(color, (ShadowRadius * 0.7 + cornerRadius) * num));
				color.A = (byte)Math.Round(0.02654 * (double)unchecked((int)c.A));
				gradientStopCollection.Add(new GradientStop(color, (ShadowRadius * 0.9 + cornerRadius) * num));
				color.A = 0;
				gradientStopCollection.Add(new GradientStop(color, (ShadowRadius + cornerRadius) * num));
				gradientStopCollection.Freeze();
				return gradientStopCollection;
			}
		}
		private System.Windows.Media.Brush[] CreateBrushes(System.Windows.Media.Color c, CornerRadius cornerRadius)
		{
			System.Windows.Media.Brush[] array = new System.Windows.Media.Brush[9]
			{
				null,
				null,
				null,
				null,
				new SolidColorBrush(c),
				null,
				null,
				null,
				null
			};
			array[4].Freeze();
			GradientStopCollection gradientStopCollection = CreateStops(c, 0.0);
			System.Windows.Media.LinearGradientBrush linearGradientBrush = new System.Windows.Media.LinearGradientBrush(gradientStopCollection, new System.Windows.Point(0.0, 1.0), new System.Windows.Point(0.0, 0.0));
			linearGradientBrush.Freeze();
			array[1] = linearGradientBrush;
			System.Windows.Media.LinearGradientBrush linearGradientBrush2 = new System.Windows.Media.LinearGradientBrush(gradientStopCollection, new System.Windows.Point(1.0, 0.0), new System.Windows.Point(0.0, 0.0));
			linearGradientBrush2.Freeze();
			array[3] = linearGradientBrush2;
			System.Windows.Media.LinearGradientBrush linearGradientBrush3 = new System.Windows.Media.LinearGradientBrush(gradientStopCollection, new System.Windows.Point(0.0, 0.0), new System.Windows.Point(1.0, 0.0));
			linearGradientBrush3.Freeze();
			array[5] = linearGradientBrush3;
			System.Windows.Media.LinearGradientBrush linearGradientBrush4 = new System.Windows.Media.LinearGradientBrush(gradientStopCollection, new System.Windows.Point(0.0, 0.0), new System.Windows.Point(0.0, 1.0));
			linearGradientBrush4.Freeze();
			array[7] = linearGradientBrush4;
			GradientStopCollection gradientStopCollection2 = ((cornerRadius.TopLeft != 0.0) ? CreateStops(c, cornerRadius.TopLeft) : gradientStopCollection);
			RadialGradientBrush radialGradientBrush = new RadialGradientBrush(gradientStopCollection2)
			{
				RadiusX = 1.0,
				RadiusY = 1.0,
				Center = new System.Windows.Point(1.0, 1.0),
				GradientOrigin = new System.Windows.Point(1.0, 1.0)
			};
			radialGradientBrush.Freeze();
			array[0] = radialGradientBrush;
			GradientStopCollection gradientStopCollection3;
			if (cornerRadius.TopRight == 0.0)
			{
				gradientStopCollection3 = gradientStopCollection;
			}
			else
			{
				gradientStopCollection3 = ((cornerRadius.TopRight != cornerRadius.TopLeft) ? CreateStops(c, cornerRadius.TopRight) : gradientStopCollection2);
			}
			RadialGradientBrush radialGradientBrush2 = new RadialGradientBrush(gradientStopCollection3)
			{
				RadiusX = 1.0,
				RadiusY = 1.0,
				Center = new System.Windows.Point(0.0, 1.0),
				GradientOrigin = new System.Windows.Point(0.0, 1.0)
			};
			radialGradientBrush2.Freeze();
			array[2] = radialGradientBrush2;
			GradientStopCollection gradientStopCollection4;
			if (cornerRadius.BottomLeft == 0.0)
			{
				gradientStopCollection4 = gradientStopCollection;
			}
			else if (cornerRadius.BottomLeft == cornerRadius.TopLeft)
			{
				gradientStopCollection4 = gradientStopCollection2;
			}
			else
			{
				gradientStopCollection4 = ((cornerRadius.BottomLeft != cornerRadius.TopRight) ? CreateStops(c, cornerRadius.BottomLeft) : gradientStopCollection3);
			}
			RadialGradientBrush radialGradientBrush3 = new RadialGradientBrush(gradientStopCollection4)
			{
				RadiusX = 1.0,
				RadiusY = 1.0,
				Center = new System.Windows.Point(1.0, 0.0),
				GradientOrigin = new System.Windows.Point(1.0, 0.0)
			};
			radialGradientBrush3.Freeze();
			array[6] = radialGradientBrush3;
			GradientStopCollection gradientStopCollection5;
			if (cornerRadius.BottomRight == 0.0)
			{
				gradientStopCollection5 = gradientStopCollection;
			}
			else if (cornerRadius.BottomRight == cornerRadius.TopLeft)
			{
				gradientStopCollection5 = gradientStopCollection2;
			}
			else if (cornerRadius.BottomRight == cornerRadius.TopRight)
			{
				gradientStopCollection5 = gradientStopCollection3;
			}
			else
			{
				gradientStopCollection5 = ((cornerRadius.BottomRight != cornerRadius.BottomLeft) ? CreateStops(c, cornerRadius.BottomRight) : gradientStopCollection4);
			}
			RadialGradientBrush radialGradientBrush4 = new RadialGradientBrush(gradientStopCollection5)
			{
				RadiusX = 1.0,
				RadiusY = 1.0,
				Center = new System.Windows.Point(0.0, 0.0),
				GradientOrigin = new System.Windows.Point(0.0, 0.0)
			};
			radialGradientBrush4.Freeze();
			array[8] = radialGradientBrush4;
			return array;
		}
		private System.Windows.Media.Brush[] GetBrushes(System.Windows.Media.Color c, CornerRadius cornerRadius)
		{
			if (_commonBrushes == null)
			{
				object resourceAccess = _resourceAccess;
				ObjectFlowControl.CheckForSyncLockOnValueType(resourceAccess);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(resourceAccess, ref lockTaken);
					if (_commonBrushes == null)
					{
						_commonBrushes = CreateBrushes(c, cornerRadius);
						_commonCornerRadius = cornerRadius;
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(resourceAccess);
					}
				}
			}
			if (c == ((SolidColorBrush)_commonBrushes[4]).Color && cornerRadius == _commonCornerRadius)
			{
				_brushes = null;
				return _commonBrushes;
			}
			if (_brushes == null)
			{
				_brushes = CreateBrushes(c, cornerRadius);
			}
			return _brushes;
		}
	}
}
