using System;
using System.Collections;
using DG.Tweening;
using Services;
using Services.EventBus;
using Services.ServiceLocator;
using TMPro;
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

        private void OnEnable()
        {
            var services = ServiceLocator.Current;

            var time = services.Get<Timer>().Time;
            var record = services.Get<HighScoreManager>().HighScore;
            
            var scoreLine = transform.Find("ScoreLine");
            if (scoreLine != null)
            {
                var scoreLineWidth = scoreLine.GetComponent<RectTransform>().rect.width;
                var scoreLineTransform = scoreLine.Find("ScoreIndicator").GetComponent<RectTransform>();
                var right = scoreLineWidth - (Mathf.Clamp(record / time, 0, 1) * scoreLineWidth);
                scoreLineTransform.offsetMax = new Vector2(-right, scoreLineTransform.offsetMax.y);
            }
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