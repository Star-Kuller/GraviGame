using System.Collections.Generic;
using System.Linq;
using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;
using UnityEngine.Events;

namespace Systems
{
    public class FinishSystem : IService
    {
        private readonly List<Finish> _finishes = new List<Finish>();
        private EventBus _eventBus;

        public FinishSystem()
        {
            Init();
        }

        private void Init()
        {
            var services = ServiceLocator.Current;
            _eventBus = services.Get<EventBus>();
            _eventBus.Subscribe(EventList.Finished, CheckAllFinishes);
        }
        public void AddFinish(Finish finish)
        {
            Debug.Log($"added {finish}");
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
