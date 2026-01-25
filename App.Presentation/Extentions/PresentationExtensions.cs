using App.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Presentation.Extentions
{
	public static class PresentationExtensions
	{
		public static List<SelectListItem> ConvertEnumToSelectListItems(Type t, LocalizationService l)
		{
			var x = new List<SelectListItem>();
			var elements = Enum.GetValues(t);
			for (int i = 0; i < elements.Length; i++)
			{
				x.Add(new SelectListItem() { Text = l.GetLocalizedHtmlString(t.Name + "." + elements.GetValue(i).ToString()), Value = (i).ToString() });

			}
			return x;
		}
	}
}
