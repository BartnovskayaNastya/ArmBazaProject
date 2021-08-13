using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArmBazaProject.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // Пришлось добавить флаги для того чтобы отличать обычную
        //  установку свойства от Undo/Redo
        static bool isUndoProcess = false;
        static bool isRedoProcess = false;

        // Пара стеков для хранения истории
        static Stack<(object Obj, string Prop, object OldValue)> undoHistory
            = new Stack<(object Obj, string Prop, object OldValue)>();

        static Stack<(object Obj, string Prop, object OldValue)> redoHistory
            = new Stack<(object Obj, string Prop, object OldValue)>();

        static void Undo()
        {
            if (undoHistory.Count == 0) return;
            var undo = undoHistory.Pop();
            UndoCommand.RaiseCanExecuteChanged();
            // Обернуто для того чтобы в случае исключения флаг всё равно снимался
            try
            {
                isUndoProcess = true;
                undo.Obj.GetType().GetProperty(undo.Prop).SetValue(undo.Obj, undo.OldValue);
            }
            finally
            {
                isUndoProcess = false;
            }
        }

        static void Redo()
        {
            if (redoHistory.Count == 0) return;
            var redo = redoHistory.Pop();
            RedoCommand.RaiseCanExecuteChanged();
            try
            {
                isRedoProcess = true;
                redo.Obj.GetType().GetProperty(redo.Prop).SetValue(redo.Obj, redo.OldValue);
            }
            finally
            {
                isRedoProcess = false;
            }
        }

        static void SaveHistory(object obj, string propertyName, object value)
        {
            if (isUndoProcess)
            {
                redoHistory.Push((obj, propertyName, value));
                RedoCommand.RaiseCanExecuteChanged();
            }
            else if (isRedoProcess)
            {
                undoHistory.Push((obj, propertyName, value));
                UndoCommand.RaiseCanExecuteChanged();
            }
            else
            {
                undoHistory.Push((obj, propertyName, value));
                UndoCommand.RaiseCanExecuteChanged();
                redoHistory.Clear();
                RedoCommand.RaiseCanExecuteChanged();
            }
        }

        static void ClearHistory()
        {
            undoHistory.Clear();
            UndoCommand.RaiseCanExecuteChanged();
            redoHistory.Clear();
            RedoCommand.RaiseCanExecuteChanged();
        }

        // Команды, которые можно выставлять в GUI
        public static DelegateCommand UndoCommand { get; }
            = new DelegateCommand(_ => Undo(), _ => undoHistory.Count > 0);
        public static DelegateCommand RedoCommand { get; }
            = new DelegateCommand(_ => Redo(), _ => redoHistory.Count > 0);
        public static DelegateCommand ClearHistoryCommand { get; }
            = new DelegateCommand(_ => ClearHistory());


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
