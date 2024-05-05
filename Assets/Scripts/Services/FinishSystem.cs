using System.Collections.Generic;
using System.Linq;
using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;

namespace Services
{
    public class FinishSystem : IService
    {
        private readonly List<Finish> _finishes = new List<Finish>();
        private EventBus.EventBus _eventBus;

        public FinishSystem()
        {
            Init();
        }

        private void Init()
        {
            var services = ServiceLocator.ServiceLocator.Current;
            _eventBus = services.Get<EventBus.EventBus>();
            _eventBus.Subscribe(EventList.Finished, CheckAllFinishes);
        }
        public void AddFinish(Finish finish)
        {
            Debug.Log($"added {finish.GetHashCode()}");
            _finishes.Add(finish);
        }

        private void CheckAllFinishes()
        {
            if (_finishes.Any(finish => !finish.IsFinished))
                return;
            Debug.Log("call EventList.Victory");
            _eventBus.CallEvent(EventList.Victory);
        }
    }
}
