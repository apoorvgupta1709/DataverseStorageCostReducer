using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DataverseStorageCostReducer.AppCode.Helpers
{
    public static class SponsorHelper
    {
        public const string SponsorUrl = "https://github.com/sponsors/apoorvgupta1709";

        public static void OpenSponsorPage()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = SponsorUrl,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open browser. Please navigate to:\n{SponsorUrl}\n\nError: {ex.Message}", "GitHub Sponsors", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void ShowSponsorDialog(IWin32Window owner, string toolName)
        {
            using (var dlg = new Form())
            {
                dlg.Text = $"Donate & Support {toolName}";
                dlg.Size = new Size(580, 460);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;
                dlg.BackColor = Color.White;

                var pnlHeader = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 85,
                    BackColor = Color.FromArgb(253, 242, 248), // soft rose/pink
                    Padding = new Padding(20, 15, 20, 10)
                };

                var lblTitle = new Label
                {
                    Text = $"💖 Support & Donate — {toolName}",
                    Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(190, 24, 93), // Rose-700
                    AutoSize = true,
                    Location = new Point(20, 15)
                };

                var lblSubtitle = new Label
                {
                    Text = "Created by Apoorv Gupta • Free & Open-Source for the Dynamics 365 community",
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    AutoSize = true,
                    Location = new Point(22, 45)
                };

                pnlHeader.Controls.Add(lblTitle);
                pnlHeader.Controls.Add(lblSubtitle);

                var txtInfo = new TextBox
                {
                    Multiline = true,
                    ReadOnly = true,
                    BorderStyle = BorderStyle.None,
                    BackColor = Color.White,
                    Font = new Font("Segoe UI", 9.5F),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    Location = new Point(25, 100),
                    Size = new Size(515, 240),
                    Text = "If this tool helped you identify storage bloat and reduce thousands of dollars in Microsoft Dataverse licensing costs, please consider donating to support continuous development!\r\n\r\n" +
                           "⭐ DONATION & SUPPORT OPTIONS:\r\n" +
                           "• ☕ $5 (Coffee) — Community Supporter\r\n" +
                           "   Help cover hosting, testing environments, and maintenance.\r\n\r\n" +
                           "• 💎 $15 — Pro Supporter\r\n" +
                           "   Automated purge schedulers, priority bug fixes, and feature requests.\r\n\r\n" +
                           "• 🏢 $50 — Enterprise Backer\r\n" +
                           "   Company sponsor recognition on GitHub repo, custom archival plugins, and 1-on-1 assistance.\r\n\r\n" +
                           "Thank you for keeping open-source Dataverse tooling alive!"
                };

                var pnlButtons = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = 65,
                    BackColor = Color.FromArgb(248, 250, 252),
                    Padding = new Padding(20, 12, 20, 12)
                };

                var btnOpen = new Button
                {
                    Text = "💖 Donate Now",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    BackColor = Color.FromArgb(219, 39, 119), // Pink-600
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(270, 38),
                    Location = new Point(20, 14),
                    Cursor = Cursors.Hand
                };
                btnOpen.FlatAppearance.BorderSize = 0;
                btnOpen.Click += (s, e) =>
                {
                    OpenSponsorPage();
                    dlg.DialogResult = DialogResult.OK;
                    dlg.Close();
                };

                var btnClose = new Button
                {
                    Text = "Maybe Later",
                    Font = new Font("Segoe UI", 9F),
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(71, 85, 105),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(110, 38),
                    Location = new Point(430, 14),
                    DialogResult = DialogResult.Cancel
                };
                btnClose.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);

                pnlButtons.Controls.Add(btnOpen);
                pnlButtons.Controls.Add(btnClose);

                dlg.Controls.Add(pnlHeader);
                dlg.Controls.Add(txtInfo);
                dlg.Controls.Add(pnlButtons);

                dlg.ShowDialog(owner);
            }
        }
    }
}
