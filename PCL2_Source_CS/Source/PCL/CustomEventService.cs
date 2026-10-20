// PCL

namespace PCL
{
	public class CustomEventService
	{
		public static readonly DependencyProperty EventsProperty = DependencyProperty.RegisterAttached("Events", typeof(CustomEventCollection), typeof(CustomEventService), new PropertyMetadata(null));
		public static readonly DependencyProperty EventTypeProperty = DependencyProperty.RegisterAttached("EventType", typeof(CustomEvent.EventType), typeof(CustomEventService), new PropertyMetadata(null));
		public static readonly DependencyProperty EventDataProperty = DependencyProperty.RegisterAttached("EventData", typeof(string), typeof(CustomEventService), new PropertyMetadata(null));
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static void SetEvents(DependencyObject d, CustomEventCollection value)
		{
			d.SetValue(EventsProperty, value);
		}
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static CustomEventCollection GetEvents(DependencyObject d)
		{
			if (d.GetValue(EventsProperty) == null)
			{
				d.SetValue(EventsProperty, new CustomEventCollection());
			}
			return (CustomEventCollection)d.GetValue(EventsProperty);
		}
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static void SetEventType(DependencyObject d, CustomEvent.EventType value)
		{
			d.SetValue(EventTypeProperty, value);
		}
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static CustomEvent.EventType GetEventType(DependencyObject d)
		{
			return (CustomEvent.EventType)Conversions.ToInteger(d.GetValue(EventTypeProperty));
		}
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static void SetEventData(DependencyObject d, string value)
		{
			d.SetValue(EventDataProperty, value);
		}
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static string GetEventData(DependencyObject d)
		{
			return Conversions.ToString(d.GetValue(EventDataProperty));
		}
	}
}
