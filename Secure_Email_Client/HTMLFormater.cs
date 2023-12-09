using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secure_Email_Client
{
    public static class HTMLFormater
    {
        public static string ConvertRichTextBoxToHtml(RichTextBox richTextBox)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<html><body>");

            richTextBox.Select(0, 1);
            Font checkFont = richTextBox.SelectionFont;
            HorizontalAlignment checkAlignment = richTextBox.SelectionAlignment;

            if (checkAlignment == HorizontalAlignment.Left)
            {
                html.Append("<p style='text-align: left;'>");
            }
            else if (checkAlignment == HorizontalAlignment.Right)
            {
                html.Append("<p style='text-align: right;'>");
            }
            else if (checkAlignment == HorizontalAlignment.Center)
            {
                html.Append("<p style='text-align: center;'>");
            }
            if (checkFont.Bold)
            {
                html.Append("<b>");
            }
            if (checkFont.Italic)
            {
                html.Append("<i>");
            }

            for (int i = 0; i < richTextBox.TextLength; i++)
            {
                richTextBox.Select(i, 1);
                Font currentFont = richTextBox.SelectionFont;
                HorizontalAlignment currentAlignment = richTextBox.SelectionAlignment;
                string currentText = richTextBox.SelectedText;

                if (checkFont.Bold == currentFont.Bold
                    && checkFont.Italic == currentFont.Italic)
                {
                    html.Append(currentText);
                    continue;
                }
                else
                {
                    if (checkFont.Italic)
                    {
                        html.Append("</i>");
                    }
                    if (checkFont.Bold)
                    {
                        html.Append("</b>");
                    }
                    checkFont = currentFont;
                }

                if (checkAlignment != currentAlignment)
                {
                    checkAlignment = currentAlignment;
                    html.Append("</p>");
                    if (currentAlignment == HorizontalAlignment.Left)
                    {
                        html.Append("<p style='text-align: left;'>");
                    }
                    else if (currentAlignment == HorizontalAlignment.Right)
                    {
                        html.Append("<p style='text-align: right;'>");
                    }
                    else if (currentAlignment == HorizontalAlignment.Center)
                    {
                        html.Append("<p style='text-align: center;'>");
                    }
                }

                if (currentFont.Bold)
                {
                    html.Append("<b>");
                }
                if (currentFont.Italic)
                {
                    html.Append("<i>");
                }

                if (currentText == "\n")
                    html.Append("<br>");
                else
                    html.Append(currentText);
            }

            html.Append("</body></html>");
            return html.ToString();
        }
    }
}
