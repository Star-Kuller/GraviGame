using System;
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
        private float _originalSizeY;
        
        private void Awake()
        {
            _uiElement = GetComponent<RectTransform>();
            _originalSizeY = _uiElement.localScale.y;
        }

        private void Start()
        {
            var services = ServiceLocator.Current;
            _eventBus = services.Get<EventBus>();
        }

        private void OnEnable()
        {
            var services = ServiceLocator.Current;
            var score = transform.Find("Score");
            var scoreText = score.GetComponent<TMP_Text>();
            
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

            var minutes = Mathf.FloorToInt(time / 60f);
            var seconds = Mathf.FloorToInt(time % 60f);
            var milliseconds = Mathf.FloorToInt((time - Mathf.Floor(time)) * 100f);
            
            var recordMinutes = Mathf.FloorToInt(record / 60f);
            var recordSeconds = Mathf.FloorToInt(record % 60f);
            var recordMilliseconds = Mathf.FloorToInt((record - Mathf.Floor(record)) * 100f);
            
            scoreText.text = $"Record: {recordMinutes:00}:{recordSeconds:00}.{recordMilliseconds:00}\n" +
                             $"Current: {minutes:00}:{seconds:00}.{milliseconds:00}";
        }

        public void MenuButton()
        {
            
        }
        
        public void RestartButton()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
        
        public void NextButton()
        {
            
        }
        
        public void ResumeButton()
        {
            _eventBus.CallEvent(EventList.ShowPauseButton);
            _uiElement.DOScaleY(0, hideAnimationDuration)
                .SetEase(Ease.InOutQuart)
                .SetUpdate(UpdateType.Normal, true)
                .OnComplete(() => _eventBus.CallEvent(EventList.Resume));
        }
    }
}