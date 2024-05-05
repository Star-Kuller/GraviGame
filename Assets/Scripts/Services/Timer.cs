using System.Diagnostics;
using Services.EventBus;
using Services.ServiceLocator;

namespace Services
{
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
            var services = ServiceLocator.ServiceLocator.Current;
            var eventBus = services.Get<EventBus.EventBus>();
        
            eventBus.Subscribe(EventList.Finished, TimerStop);
            eventBus.Subscribe(EventList.Pause, TimerStop);
            eventBus.Subscribe(EventList.Loose, TimerStop);
            eventBus.Subscribe(EventList.Victory, TimerStop);
            eventBus.Subscribe(EventList.Finished, TimerStop);
        
            eventBus.Subscribe(EventList.Start, TimerStart);
            eventBus.Subscribe(EventList.Resume, TimerStart);
        }

        private void TimerStart()
        {
            _stopwatch.Start();
            UnityEngine.Time.timeScale = 1f;
        }

        private void TimerStop()
        {
            _stopwatch.Stop();
            UnityEngine.Time.timeScale = 0;
        }
    }
}
