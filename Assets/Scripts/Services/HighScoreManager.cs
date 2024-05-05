using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class HighScoreManager : IService
    {
        private Timer _timer;

        public HighScoreManager()
        {
            Init();
        }

        private void Init()
        {
            var services = ServiceLocator.ServiceLocator.Current;
            var eventBus = services.Get<EventBus.EventBus>();
            eventBus.Subscribe(EventList.Victory, AutoSave);
            _timer = services.Get<Timer>();
        }
        
        
        public float HighScore => Get(SceneManager.GetActiveScene().buildIndex);

        private void AutoSave()
        {
            Save(_timer.Time);
        }
        
        public void Save(float time)
        {
            var key = $"LevelScore_{SceneManager.GetActiveScene().buildIndex}";
            var currentBestScore = PlayerPrefs.GetFloat(key, float.MaxValue);
            
            if (time > currentBestScore) return;
            
            Debug.Log($"saved record {key} : {time}");
            PlayerPrefs.SetFloat(key, time);
            PlayerPrefs.Save();
        }

        private float Get(int levelIndex)
        {
            var key = $"LevelScore_{levelIndex}";
            var val = PlayerPrefs.GetFloat(key, 0);
            Debug.Log($"get record {key} : {val}");
            return val;
        }
        
    }
}