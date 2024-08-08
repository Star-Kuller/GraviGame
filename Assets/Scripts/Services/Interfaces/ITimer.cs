using Services.ServiceLocator;

namespace Services.Interfaces
{
    public interface ITimer : IService
    {
        public float Time { get; }
        public bool TimerIsRunning { get; }
        public bool TimerStarted { get; }

    }
}