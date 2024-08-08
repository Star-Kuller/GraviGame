using DG.Tweening;
using Services.EventBus;
using Services.Interfaces;
using Services.ServiceLocator;
using UnityEngine;

namespace View
{
    public class PauseButtonView : MonoBehaviour
    {
        [SerializeField] private float animationDuration = 0.45f;
        private IEventBus _eventBus;
        private RectTransform _uiElement;
        private float _originalSizeY;
        
        private void Awake()
        {
            _uiElement = GetComponent<RectTransform>();
            _originalSizeY = _uiElement.localScale.y;
        }
        
        private void Start()
        {
            var services = ServiceLocator.Current;
            _eventBus = services.Get<IEventBus>();
            _eventBus.Subscribe(EventList.Victory, HideAnimation);
            _eventBus.Subscribe(EventList.Loose, HideAnimation);
            _eventBus.Subscribe(EventList.ShowPauseButton, ShowAnimation);
        }

        public void Pause()
        {
            _eventBus.CallEvent(EventList.Pause);
            HideAnimation();
        }

        private void HideAnimation()
        {
            _uiElement.DOScaleY(0, animationDuration)
                .SetEase(Ease.InOutQuart)
                .SetUpdate(UpdateType.Normal, true);
        }
        
        private void ShowAnimation()
        {
            _uiElement.DOScaleY(_originalSizeY, animationDuration)
                .SetEase(Ease.InOutQuart)
                .SetUpdate(UpdateType.Normal, true);
        }
    }
}
