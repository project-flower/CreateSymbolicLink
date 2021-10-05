using CreateSymbolicLink.Properties;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Win32Api;

namespace CreateSymbolicLink
{
    class Program
    {
        #region Internal Methods

        static int Main(string[] args)
        {
            Application.EnableVisualStyles();

            if (args.Length < 2)
            {
                string message = string.Format("コマンドの構文が誤っています。{0}{0}{1}", Environment.NewLine, Resources.Description);
                ShowErrorMessage(message);
                return -1;
            }

            uint flags;

            if (args.Length > 2)
            {
                string argument = args[2];

                try
                {
                    flags = uint.Parse(argument);
                }
                catch
                {
                    ShowErrorMessage($"無効なスイッチです - \"{argument}\"");
                    return -1;
                }
            }
            else
            {
                flags = 0;
            }

            string symlinkFileName = args[0];
            string targetFileName = args[1];

            if (!Kernel32.CreateSymbolicLink(symlinkFileName, targetFileName, flags))
            {
                ShowErrorMessage(new Win32Exception(Marshal.GetLastWin32Error()).Message);
                return -1;
            }

            ShowMessage($"{symlinkFileName} <<==>> {targetFileName} のシンボリック リンクが作成されました", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return 0;
        }

        #endregion

        #region Private Methods

        private static void ShowErrorMessage(string message)
        {
            ShowMessage(message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static DialogResult ShowMessage(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, "CreateSymbolicLink", buttons, icon);
        }

        #endregion
    }
}
