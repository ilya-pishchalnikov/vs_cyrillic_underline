using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Task = System.Threading.Tasks.Task;

namespace ssms_cyryllic_underline
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommandCyrillicUnderline
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("77480475-ca21-42e0-a3b4-354000c7c5b6");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCyrillicUnderline"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CommandCyrillicUnderline(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CommandCyrillicUnderline Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in CommandCyrillicUnderline's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new CommandCyrillicUnderline(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var dte2 = Util.GetDTE2();

            IVsTextView textView = null;

            if (!dte2.TryGetTextView(out textView))
            {
                return;
            }


            var result = textView.GetBuffer(out var textLines);

            textLines.GetLastLineIndex(out var lastLine, out var lastIndex);

            textView.GetTextStream(0, 0, lastLine, lastIndex, out var text);

            var matches = Regex.Matches(text, "(@|@@|#|##|)[a-zA-Zа-яА-Я0-9_]+");

            var mixedWords = new List<Match>();

            foreach(Match match in matches)
            {
                if (Regex.IsMatch(match.Value, "[а-яА-Я]") && Regex.IsMatch(match.Value, "[a-zA-Z]"))
                {
                    mixedWords.Add(match);
                }
            }

            var mixedWordsString = string.Empty;

            for (int i = 0; i < mixedWords.Count; i++)
            {
                textView.GetLineAndColumn(mixedWords[i].Index, out var line, out var index);
                mixedWordsString += $"{line + 1}:{index + 1} {mixedWords[i].Value}";
                if (i < mixedWords.Count - 1)
                {
                    mixedWordsString += "\n";
                }
            }

            //textView.


            string message = mixedWordsString;
            string title = "Identifiers with mixed Latin and Cyrillic symbols";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.package,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
