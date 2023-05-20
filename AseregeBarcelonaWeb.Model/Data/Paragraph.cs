using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AseregeBarcelonaWeb.Model.Data
{
	public class Paragraph
	{
		private string title;			
		private List<string> paragraph;

		public Paragraph(string title, List<string> paragraph)
		{
			this.title = $"<h2>{System.Net.WebUtility.HtmlEncode(title)}</h2>";
			this.paragraph = paragraph;
								
		}
		public string GetParagraph() 
		{
			string text = title;
			foreach (string line in paragraph)
			{
				text = string.Concat(text, $"<p>{System.Net.WebUtility.HtmlEncode(line)}</p>");
			}
			return text;
		}
	}
}
