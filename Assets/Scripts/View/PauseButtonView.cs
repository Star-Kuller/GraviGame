using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;

namespace View
{
    public class PauseButtonView : MonoBehaviour
    {
        private EventBus _eventBus;
        private void Start()
        {
            var services = ServiceLocator.Current;
            _eventBus = services.Get<EventBus>();
        }

        public void Pause()
        {
            _eventBus.CallEvent(EventList.Pause);
        }
    }
}
