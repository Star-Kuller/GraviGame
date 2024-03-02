
using System.Diagnostics;
using Services.EventBus;
using Services.ServiceLocator;

public class Timer : IService
{
    private readonly Stopwatch _stopwatch;
    public float Time => (float)_stopwatch.Elapsed.TotalSeconds;
    public bool TimerIsRunning => _stopwatch.IsRunning;

    public Timer()
    {
        _stopwatch = new Stopwatch();
        Init();
    }

    private void Init()
    {
        var services = ServiceLocator.Current;
        var eventBus = services.Get<EventBus>();
        
        eventBus.Subscribe(EventList.Finished, TimerStop);
        eventBus.Subscribe(EventList.Pause, TimerStop);
        eventBus.Subscribe(EventList.Loose, TimerStop);
        eventBus.Subscribe(EventList.Victory, TimerStop);
        eventBus.Subscribe(EventList.Finished, TimerStop);
        
        eventBus.Subscribe(EventList.Start, TimerStart);
        eventBus.Subscribe(EventList.Resume, TimerStart);
    }

    public void TimerStart()
    {
        _stopwatch.Start();
    }

    public void TimerStop()
    {
        _stopwatch.Stop();
    }
    
}
