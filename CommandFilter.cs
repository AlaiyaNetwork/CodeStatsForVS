using System.Threading;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text.Editor;

namespace CodeStatsForVS
{
    /// <summary>
    /// Обработка ввода пользователя.
    /// </summary>
    internal class CommandFilter : IOleCommandTarget
    {
        #region Переменные

        /// <summary>
        /// Экземпляр <see cref="IWpfTextView"/>, для получения информации о введенном тексте.
        /// </summary>
        private readonly IWpfTextView _textView;

        /// <summary>
        /// Задача на вызов отравки данных на сервер.
        /// </summary>
        private Task _publisher = null;

        /// <summary>
        /// Экземпляр <see cref="Pulse"/> для работы с API CodeStats.
        /// </summary>
        private readonly Pulse pulse = new();

        #endregion

        #region Свойства

        /// <summary>
        /// Следующая команда по конвейеру.
        /// </summary>
        internal IOleCommandTarget NextTarget { get; set; }

        /// <summary>
        /// Флаг, указывающий на то, что данный фильтр был добавлен к событию.
        /// </summary>
        internal bool Added { get; set; }

        #endregion

        public CommandFilter(IWpfTextView textView) 
            => _textView = textView;

        /// <summary>
        /// Метод вызова текущего <see cref="CommandFilter"/>.
        /// </summary>
        /// <param name="pguidCmdGroup">
        /// Уникальный идентификатор группы команд; может иметь значение <see langword="null"/> для указания стандартной группы.
        /// </param>
        /// <param name="nCmdID">
        /// Команда, которая должна быть выполнена. Эта команда должна принадлежать группе, указанной с помощью <paramref name="pguidCmdGroup"/>.
        /// </param>
        /// <param name="nCmdexecopt">
        /// Указывает, как объект должен выполнять команду. Возможные значения берутся из перечислений <see cref="OLECMDEXECOPT"/>  и <see langword="OLECMDID_WINDOWSTATE_FLAG"/>.
        /// </param>
        /// <param name="pvaIn">
        /// Указатель на структуру <see langword="VARIANTARG"/>, содержащую входные аргументы. Этот параметр может быть равен <see langword="null"/>.
        /// </param>
        /// <param name="pvaOut">
        /// Указатель на структуру <see langword="VARIANTARG"/> для получения выходных данных команды. Этот параметр может быть равен <see langword="null"/>.
        /// </param>
        /// <returns>
        /// Этот метод возвращает <see cref="VSConstants.S_OK"/> при успешном выполнении. 
        /// Другие возможные возвращаемые значения включают следующее:
        /// <see langword="OLECMDERR_E_UNKNOWNGROUP" />, <see langword="OLECMDERR_E_NOTSUPPORTED" />, <see langword="OLECMDERR_E_DISABLED" />, 
        /// <see langword="OLECMDERR_E_NOHELP" />, <see langword="OLECMDERR_E_CANCELED" />
        /// </returns>
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

        /// <summary>
        /// Запрашивает у объекта статус одной или нескольких команд, сгенерированных событиями пользовательского интерфейса.
        /// </summary>
        /// <param name="pguidCmdGroup">
        /// Уникальный идентификатор группы команд; может иметь значение <see langword="null"/> для указания стандартной группы.
        /// </param>
        /// <param name="cCmds">Количество команд в массиве <paramref name="prgCmds"/>.</param>
        /// <param name="prgCmds">
        /// Массив структур <see langword="OLECMD" />, которые указывают команды, для которых вызывающему требуется информация о состоянии. 
        /// Этот метод заполняет элемент <see langword="cmdf" /> каждой структуры значениями, взятыми из перечисления <see langword="OLECMDF" />.
        /// </param>
        /// <param name="pCmdText">
        /// Указатель на структуру <see cref="OLECMDTEXT"/>, в которой возвращается имя и/или информация о состоянии одной команды. 
        /// Этот параметр может иметь значение <see langword="null"/>, чтобы указать, что вызывающему абоненту не нужна эта информация.
        /// </param>
        /// <returns>
        /// Этот метод возвращает <see cref="VSConstants.S_OK"/> при успешном выполнении.
        /// Другие возможные возвращаемые значения включают следующее:
        /// <see cref="VSConstants.E_FAIL" />, <see cref="VSConstants.E_UNEXPECTED" />, 
        /// <see cref="VSConstants.E_POINTER" />, <see langword="OLECMDERR_E_UNKNOWNGROUP" />.
        /// </returns>
        int IOleCommandTarget.QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText) 
            => NextTarget.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
    }
}
