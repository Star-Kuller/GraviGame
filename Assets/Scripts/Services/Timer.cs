using System.Diagnostics;
using Services.EventBus;
using Services.Interfaces;
using Services.ServiceLocator;

namespace Services
{
    public class Timer : ITimer
    {
        private readonly Stopwatch _stopwatch;
        public float Time => (float)_stopwatch.Elapsed.TotalSeconds;
        public bool TimerIsRunning => _stopwatch.IsRunning;
        public bool TimerStarted { get; private set; } = false;

        public Timer()
        {
            _stopwatch = new Stopwatch();
            Init();
        }

        private void Init()
        {
            var services = ServiceLocator.ServiceLocator.Current;
            var eventBus = services.Get<IEventBus>();
        
            eventBus.Subscribe(EventList.Finished, TimerStop);
            eventBus.Subscribe(EventList.Pause, TimerStop);
            eventBus.Subscribe(EventList.Loose, TimerStop);
            eventBus.Subscribe(EventList.Victory, TimerStop);
            eventBus.Subscribe(EventList.Finished, TimerStop);
        
            eventBus.Subscribe(EventList.Start, TimerStart);
            eventBus.Subscribe(EventList.Resume, TimerResume);
        }

        private void TimerResume()
        {
            if (TimerStarted)
                TimerStart();
        }

        private void TimerStart()
        {
            _stopwatch.Start();
            TimerStarted = true;
            UnityEngine.Time.timeScale = 1f;
        }

        private void TimerStop()
        {
            _stopwatch.Stop();
            UnityEngine.Time.timeScale = 0;
        }
    }
}
