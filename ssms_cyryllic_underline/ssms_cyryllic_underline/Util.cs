using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ssms_cyryllic_underline
{
    internal static class Util
    {
        public static EnvDTE80.DTE2 GetDTE2()
        {
            return (EnvDTE80.DTE2)Package.GetGlobalService(typeof(SDTE));
        }

        public static Boolean TryGetTextView(this EnvDTE80.DTE2 dte2, out IVsTextView textView)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (dte2.ActiveDocument == null)
            {
                textView = null;

                return false;
            }

            var aDocIsOpen = VsShellUtilities.IsDocumentOpen(
                provider: new ServiceProvider((Microsoft.VisualStudio.OLE.Interop.IServiceProvider)dte2),
                fullPath: dte2.ActiveDocument.FullName,
                logicalView: Guid.Empty,
                hierarchy: out var uiHierarchy,
                itemID: out var itemID,
                windowFrame: out var windowFrame
            );

            if (!aDocIsOpen)
            {
                textView = null;

                return false;
            }

            windowFrame.GetCodeWindow().Value.GetLastActiveView(out textView);
           
            return true;
        }

        /// <summary>
        /// https://github.com/VsVim/VsVim/blob/d7b3e1a79a6d06cdae5e0334e09f9bbf5388e7df/Src/VsVimShared/Extensions.cs#L500
        /// </summary>
        /// <param name="vsWindowFrame"></param>
        /// <returns></returns>
        public static Result<IVsCodeWindow> GetCodeWindow(this IVsWindowFrame vsWindowFrame)
        {
            var iid = typeof(IVsCodeWindow).GUID;
            var ptr = IntPtr.Zero;

            try
            {
                var hr = vsWindowFrame.QueryViewInterface(ref iid, out ptr);

                if (ErrorHandler.Failed(hr))
                {
                    return Result.CreateError(hr);
                }

                return Result.CreateSuccess((IVsCodeWindow)Marshal.GetObjectForIUnknown(ptr));
            }
            catch (Exception e)
            {
                // Venus will throw when querying for the code window
                return Result.CreateError(e);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.Release(ptr);
                }
            }
        }
    }
}
