using System.Threading;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text.Editor;

namespace CodeStatsForVS
{
    internal class CommandFilter : IOleCommandTarget
    {
        private IWpfTextView _textView;

        internal IOleCommandTarget NextTarget { get; set; }

        internal bool Added { get; set; }

        private IAdornmentLayer _adornmentLayer;

        private Task _publisher = null;

        private Pulse.Pulse pulse = new();


        public CommandFilter(IWpfTextView textView)
        {
            _textView = textView;
            _adornmentLayer = _textView.GetAdornmentLayer("CodeStatsClientLayer");
        }

        int IOleCommandTarget.Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            if (nCmdID != (uint)VSConstants.VSStd2KCmdID.TYPECHAR && nCmdID != (uint)VSConstants.VSStd2KCmdID.BACKSPACE)
                return NextTarget.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);

            pulse.IncrementExperience(_textView.TextBuffer.ContentType.DisplayName);

            if (_publisher == null)
            {
                _publisher = Task.Factory.StartNew(async () =>
                {
                    Thread.Sleep(10 * 1000);

                    await pulse.ExecuteAsync();
                    _publisher = null;
                });
            }

            return NextTarget.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
        }

        int IOleCommandTarget.QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            return NextTarget.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }
    }
}
