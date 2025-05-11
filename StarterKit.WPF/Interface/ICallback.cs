using CommunityToolkit.Mvvm.ComponentModel;

namespace StarterKit.WPF.Interface
{

    public interface ICallback
    {
        ObservableObject ObservableObject { get; }

        event Action OnClose;
        void Subscribe(Action callbeck)
        {
            OnClose += callbeck;
        }
    }

    public interface ICallback<T>
    {
        ObservableObject ObservableObject { get; }

        event Action<T> OnClose;
        void Subscribe(Action<T> callbeck)
        {
            OnClose += callbeck;
        }
    }

    public interface ICallbackAsync
    {
        ObservableObject ObservableObject { get; }

        event Func<Task> OnClose;
        void Subscribe(Func<Task> callback)
        {
            OnClose += callback;
        }

    }

    public interface ICallbackAsync<T>
    {
        ObservableObject ObservableObject { get; }

        event Func<T, Task> OnClose;
        void Subscribe(Func<T, Task> callbeck)
        {
            OnClose += callbeck;
        }

    }
}
