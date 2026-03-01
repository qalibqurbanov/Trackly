using System.Drawing;
using System.Windows.Forms;

namespace Trackly.UI.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabControl2 = new TabControl();
            tabPage2 = new TabPage();
            control_reddit_manage_progressbar_checkStatus = new ProgressBar();
            control_reddit_manage_button_check = new Button();
            control_reddit_manage_listview_subreddits = new ListView();
            control_reddit_manage_menuFor_listview_subreddits = new ContextMenuStrip(components);
            toolStripMenuItem_Remove = new ToolStripMenuItem();
            control_reddit_manage_textbox_url = new TextBox();
            control_reddit_manage_button_add = new Button();
            tabPage4 = new TabPage();
            control_reddit_logs_listview_logs = new ListView();
            tabPage3 = new TabPage();
            control_reddit_feed_textbox_search = new TextBox();
            control_reddit_feed_combobox_subredditList = new ComboBox();
            control_reddit_feed_listview_feeds = new ListView();
            control_reddit_feed_menuFor_listview_feeds = new ContextMenuStrip(components);
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage2.SuspendLayout();
            control_reddit_manage_menuFor_listview_subreddits.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1391, 948);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tabControl2);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1383, 910);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Reddit";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage2);
            tabControl2.Controls.Add(tabPage4);
            tabControl2.Controls.Add(tabPage3);
            tabControl2.Dock = DockStyle.Fill;
            tabControl2.Location = new Point(3, 3);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(1377, 904);
            tabControl2.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(control_reddit_manage_progressbar_checkStatus);
            tabPage2.Controls.Add(control_reddit_manage_button_check);
            tabPage2.Controls.Add(control_reddit_manage_listview_subreddits);
            tabPage2.Controls.Add(control_reddit_manage_textbox_url);
            tabPage2.Controls.Add(control_reddit_manage_button_add);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1369, 866);
            tabPage2.TabIndex = 0;
            tabPage2.Text = "Manage";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // control_reddit_manage_progressbar_checkStatus
            // 
            control_reddit_manage_progressbar_checkStatus.Location = new Point(6, 74);
            control_reddit_manage_progressbar_checkStatus.Name = "control_reddit_manage_progressbar_checkStatus";
            control_reddit_manage_progressbar_checkStatus.Size = new Size(1357, 10);
            control_reddit_manage_progressbar_checkStatus.TabIndex = 8;
            // 
            // control_reddit_manage_button_check
            // 
            control_reddit_manage_button_check.Location = new Point(6, 43);
            control_reddit_manage_button_check.Name = "control_reddit_manage_button_check";
            control_reddit_manage_button_check.Size = new Size(1357, 31);
            control_reddit_manage_button_check.TabIndex = 7;
            control_reddit_manage_button_check.Text = "Check";
            control_reddit_manage_button_check.UseVisualStyleBackColor = true;
            control_reddit_manage_button_check.Click += control_reddit_manage_button_check_Click;
            // 
            // control_reddit_manage_listview_subreddits
            // 
            control_reddit_manage_listview_subreddits.ContextMenuStrip = control_reddit_manage_menuFor_listview_subreddits;
            control_reddit_manage_listview_subreddits.Dock = DockStyle.Bottom;
            control_reddit_manage_listview_subreddits.Location = new Point(3, 90);
            control_reddit_manage_listview_subreddits.Name = "control_reddit_manage_listview_subreddits";
            control_reddit_manage_listview_subreddits.Size = new Size(1363, 773);
            control_reddit_manage_listview_subreddits.TabIndex = 6;
            control_reddit_manage_listview_subreddits.UseCompatibleStateImageBehavior = false;
            // 
            // control_reddit_manage_menuFor_listview_subreddits
            // 
            control_reddit_manage_menuFor_listview_subreddits.ImageScalingSize = new Size(24, 24);
            control_reddit_manage_menuFor_listview_subreddits.Items.AddRange(new ToolStripItem[] { toolStripMenuItem_Remove });
            control_reddit_manage_menuFor_listview_subreddits.Name = "control_reddit_manage_menuFor_listview__subreddits";
            control_reddit_manage_menuFor_listview_subreddits.Size = new Size(241, 69);
            control_reddit_manage_menuFor_listview_subreddits.Click += control_reddit_manage_menuFor_listview_subreddits_Remove_Click;
            // 
            // toolStripMenuItem_Remove
            // 
            toolStripMenuItem_Remove.Name = "toolStripMenuItem_Remove";
            toolStripMenuItem_Remove.Size = new Size(240, 32);
            toolStripMenuItem_Remove.Text = "Remove";
            // 
            // control_reddit_manage_textbox_url
            // 
            control_reddit_manage_textbox_url.Location = new Point(6, 6);
            control_reddit_manage_textbox_url.Name = "control_reddit_manage_textbox_url";
            control_reddit_manage_textbox_url.Size = new Size(1214, 31);
            control_reddit_manage_textbox_url.TabIndex = 3;
            // 
            // control_reddit_manage_button_add
            // 
            control_reddit_manage_button_add.Location = new Point(1226, 6);
            control_reddit_manage_button_add.Name = "control_reddit_manage_button_add";
            control_reddit_manage_button_add.Size = new Size(137, 31);
            control_reddit_manage_button_add.TabIndex = 5;
            control_reddit_manage_button_add.Text = "Add";
            control_reddit_manage_button_add.UseVisualStyleBackColor = true;
            control_reddit_manage_button_add.Click += control_reddit_manage_button_add_Click;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(control_reddit_logs_listview_logs);
            tabPage4.Location = new Point(4, 34);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1369, 866);
            tabPage4.TabIndex = 2;
            tabPage4.Text = "Logs";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // control_reddit_logs_listview_logs
            // 
            control_reddit_logs_listview_logs.Dock = DockStyle.Fill;
            control_reddit_logs_listview_logs.Location = new Point(3, 3);
            control_reddit_logs_listview_logs.Name = "control_reddit_logs_listview_logs";
            control_reddit_logs_listview_logs.Size = new Size(1363, 860);
            control_reddit_logs_listview_logs.TabIndex = 7;
            control_reddit_logs_listview_logs.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(control_reddit_feed_textbox_search);
            tabPage3.Controls.Add(control_reddit_feed_combobox_subredditList);
            tabPage3.Controls.Add(control_reddit_feed_listview_feeds);
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1369, 866);
            tabPage3.TabIndex = 3;
            tabPage3.Text = "Feed";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // control_reddit_feed_textbox_search
            // 
            control_reddit_feed_textbox_search.Location = new Point(6, 45);
            control_reddit_feed_textbox_search.Name = "control_reddit_feed_textbox_search";
            control_reddit_feed_textbox_search.Size = new Size(1357, 31);
            control_reddit_feed_textbox_search.TabIndex = 9;
            control_reddit_feed_textbox_search.TextChanged += control_reddit_feed_textbox_search_TextChanged;
            // 
            // control_reddit_feed_combobox_subredditList
            // 
            control_reddit_feed_combobox_subredditList.FormattingEnabled = true;
            control_reddit_feed_combobox_subredditList.Location = new Point(6, 6);
            control_reddit_feed_combobox_subredditList.Name = "control_reddit_feed_combobox_subredditList";
            control_reddit_feed_combobox_subredditList.Size = new Size(1357, 33);
            control_reddit_feed_combobox_subredditList.TabIndex = 8;
            control_reddit_feed_combobox_subredditList.SelectedIndexChanged += Control_reddit_feed_combobox_subredditList_SelectedIndexChanged;
            // 
            // control_reddit_feed_listview_feeds
            // 
            control_reddit_feed_listview_feeds.ContextMenuStrip = control_reddit_feed_menuFor_listview_feeds;
            control_reddit_feed_listview_feeds.Dock = DockStyle.Bottom;
            control_reddit_feed_listview_feeds.Location = new Point(3, 82);
            control_reddit_feed_listview_feeds.Name = "control_reddit_feed_listview_feeds";
            control_reddit_feed_listview_feeds.Size = new Size(1363, 781);
            control_reddit_feed_listview_feeds.TabIndex = 7;
            control_reddit_feed_listview_feeds.UseCompatibleStateImageBehavior = false;
            // 
            // control_reddit_feed_menuFor_listview_feeds
            // 
            control_reddit_feed_menuFor_listview_feeds.ImageScalingSize = new Size(24, 24);
            control_reddit_feed_menuFor_listview_feeds.Name = "control_reddit_feed_menuFor_listview_feeds";
            control_reddit_feed_menuFor_listview_feeds.Size = new Size(61, 4);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1391, 948);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_LoadAsync;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabControl2.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            control_reddit_manage_menuFor_listview_subreddits.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabControl tabControl2;
        private TabPage tabPage2;
        private ListView control_reddit_manage_listview_subreddits;
        private TextBox control_reddit_manage_textbox_url;
        private Button control_reddit_manage_button_add;
        private TabPage tabPage4;
        private TabPage tabPage3;
        private ListView control_reddit_logs_listview_logs;
        private ListView control_reddit_feed_listview_feeds;
        private Button control_reddit_manage_button_check;
        private ProgressBar control_reddit_manage_progressbar_checkStatus;
        private ComboBox control_reddit_feed_combobox_subredditList;
        private TextBox control_reddit_feed_textbox_search;
        private ContextMenuStrip control_reddit_manage_menuFor_listview_subreddits;
        private ContextMenuStrip control_reddit_feed_menuFor_listview_feeds;
        private ToolStripMenuItem toolStripMenuItem_Remove;
    }
}