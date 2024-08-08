using Services.EventBus;
using Services.Interfaces;
using Services.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class HighScoreManager : IHighScoreManager
    {
        private ITimer _timer;
        private const string KeyName = "LEVEL_SCORE";

        public HighScoreManager()
        {
            Init();
        }

        private void Init()
        {
            var services = ServiceLocator.ServiceLocator.Current;
            var eventBus = services.Get<IEventBus>();
            eventBus.Subscribe(EventList.Victory, AutoSave);
            _timer = services.Get<ITimer>();
        }
        
        
        public float HighScore => Get(SceneManager.GetActiveScene().buildIndex);

        private void AutoSave()
        {
            Save(_timer.Time);
        }
        
        public void Save(float time)
        {
            var key = $"{KeyName}_{SceneManager.GetActiveScene().buildIndex}";
            var currentBestScore = PlayerPrefs.GetFloat(key, float.MaxValue);
            
            if (time > currentBestScore) return;
            
            Debug.Log($"saved record {key} : {time}");
            PlayerPrefs.SetFloat(key, time);
            PlayerPrefs.Save();
        }

        public float Get(int levelIndex)
        {
            var key = $"{KeyName}_{levelIndex}";
            var val = PlayerPrefs.GetFloat(key, 0);
            Debug.Log($"get record {key} : {val}");
            return val;
        }
        
    }
}