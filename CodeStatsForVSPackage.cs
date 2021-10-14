global using System;

global using Community.VisualStudio.Toolkit;

global using Microsoft.VisualStudio.Shell;

global using Task = System.Threading.Tasks.Task;

using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Threading;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace CodeStatsForVS
{
    /// <summary>
    /// Класс инициализации пакета.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.CodeStatsForVSString)]
    [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "CodeStats", "General", 0, 0, true)]
    [ProvideProfile(typeof(OptionsProvider.GeneralOptions), "CodeStats", "General", 0, 0, true)]
    [Export(typeof(IVsTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    public sealed class CodeStatsForVSPackage : ToolkitPackage, IVsTextViewCreationListener
    {
        [Export(typeof(AdornmentLayerDefinition))]
        [Name("CodeStatsClientLayer")]
        [TextViewRole(PredefinedTextViewRoles.Editable)]
        internal AdornmentLayerDefinition m_multieditAdornmentLayer = null;

        [Import(typeof(IVsEditorAdaptersFactoryService))]
        internal IVsEditorAdaptersFactoryService editorFactory = null;

        /// <summary>
        /// Вызывается, когда адаптер <see cref="IVsTextView"/> был создан и инициализирован.
        /// </summary>
        /// <param name="textViewAdapter">Недавно созданный и инициализированный адаптер текстового представления.</param>
        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            IWpfTextView textView = editorFactory.GetWpfTextView(textViewAdapter);
            if (textView == null)
                return;

            AddCommandFilter(textViewAdapter, new CommandFilter(textView));
        }

        /// <summary>
        /// Добавляет <see cref="CommandFilter"/> к <see cref="IVsTextView"/>.
        /// </summary>
        /// <param name="viewAdapter">Ссылка на <see cref="IVsTextView"/>.</param>
        /// <param name="commandFilter">Ссылка на <see cref="CommandFilter"/>.</param>
        private void AddCommandFilter(IVsTextView viewAdapter, CommandFilter commandFilter)
        {
            if (commandFilter.Added)
            {
                return;
            }

            int hr = viewAdapter.AddCommandFilter(commandFilter, out IOleCommandTarget next);

            if (hr == VSConstants.S_OK)
            {
                commandFilter.Added = true;

                if (next != null)
                    commandFilter.NextTarget = next;
            }
        }

        /// <summary>
        /// Инициализация данного пакета.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
        }
    }
}