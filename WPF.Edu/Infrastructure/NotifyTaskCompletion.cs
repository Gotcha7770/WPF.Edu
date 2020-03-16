using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WPF.Edu.Infrastructure
{
    /// <summary>
    /// See <see href="https://msdn.microsoft.com/ru-ru/magazine/dn605875.aspx"/>
    /// </summary>
    public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        public NotifyTaskCompletion(Task<TResult> task)
        {
            Task = task ?? throw new ArgumentNullException(nameof(task));

            if (!task.IsCompleted)
            {
                _ = WatchTaskAsync(task);
            }
        }

        public Task<TResult> Task { get; }

        public TaskStatus Status => Task.Status;

        public bool IsCompleted => Task.IsCompleted;

        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;

        public TResult Result => IsSuccessfullyCompleted ? Task.Result : default;

        public AggregateException Exception => Task.Exception;

        public Exception InnerException => Exception?.InnerException;

        public string ErrorMessage => InnerException?.Message;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch //???
            { }

            OnPropertyChanged("Status");
            OnPropertyChanged("IsCompleted");

            if (task.IsFaulted)
            {
                OnPropertyChanged("Exception");
                OnPropertyChanged("InnerException");
                OnPropertyChanged("ErrorMessage");
            }
            else
            {
                OnPropertyChanged("IsSuccessfullyCompleted");
                OnPropertyChanged("Result");
            }
        }
    }
}
