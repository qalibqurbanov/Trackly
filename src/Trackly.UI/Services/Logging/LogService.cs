using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Trackly.UI.Services.Logging
{
    public class LogService
    {
        // ListView instance must be passed when logging
        public static void AddLog(ListView listView, string eventType, string context)
        {
            if (listView == null)
                throw new ArgumentNullException(nameof(listView));

            if (string.IsNullOrWhiteSpace(context))
                context = "(No context)";

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            ListViewItem logItem = new ListViewItem(eventType);
            logItem.SubItems.Add(context);
            logItem.SubItems.Add(timestamp);

            listView.Items.Add(logItem);

            // Optional: auto-scroll
            listView.EnsureVisible(listView.Items.Count - 1);
        }
    }
}
