using System.Drawing;
using System.Windows.Forms;

namespace Trackly.UI.Helpers.Forms;

public class FormStylingUtils
{
    public static void ConfigureListView(ListView listView, string[] columnTitles, int[] columnPercents)
    {
        listView.View = View.Details;
        listView.FullRowSelect = true;
        listView.GridLines = true;
        listView.MultiSelect = false;
        listView.HideSelection = false;
        listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;

        listView.Columns.Clear(); // Kohne/Movcud sutunlari silirik, duplicate sutun olmamagi ucun.

        for (int i = 0; i < columnTitles.Length; i++)
        {
            listView.Columns.Add(columnTitles[i]);
        }

        int totalWidth = listView.ClientSize.Width;
        int totalPercent = 0;
        foreach (var p in columnPercents)
        {
            totalPercent += p;
        }

        for (int i = 0; i < columnPercents.Length; i++)
        {
            listView.Columns[i].Width = totalWidth * columnPercents[i] / totalPercent;
        }

        listView.BackColor = Color.White;
        listView.ForeColor = Color.Black;
    }
}