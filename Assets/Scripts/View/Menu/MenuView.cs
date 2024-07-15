using System.Collections;
using DG.Tweening;
using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private float hideAnimationDuration = 0.45f;
        
        private EventBus _eventBus;
        private RectTransform _uiElement;

        private void Awake()
        {
            _uiElement = GetComponent<RectTransform>();
        }

        private void Start()
        {
            var services = ServiceLocator.Current;
            _eventBus = services.Get<EventBus>();
        }

        public void MenuButton()
        {
            StartCoroutine(LoadMenu());
        }
        
        public void RestartButton()
        {
            StartCoroutine(ReloadLevel());
        }
        
        public void NextButton()
        {
            StartCoroutine(LoadNextLevel());
        }

        public void ResumeButton()
        {
            _eventBus.CallEvent(EventList.ShowPauseButton);
            _uiElement.DOScaleY(0, hideAnimationDuration)
                .SetEase(Ease.InOutQuart)
                .SetUpdate(UpdateType.Normal, true)
                .OnComplete(() => _eventBus.CallEvent(EventList.Resume));
        }

        private static IEnumerator LoadMenu()
        {
            yield return new WaitForSecondsRealtime(0.4f);
            SceneManager.LoadScene(0);
        }
        
        private static IEnumerator ReloadLevel()
        {
            yield return new WaitForSecondsRealtime(0.4f);
            var sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
        
        private static IEnumerator LoadNextLevel()
        {
            yield return new WaitForSecondsRealtime(0.4f);
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }
}