using Services;
using Services.Interfaces;
using Services.ServiceLocator;
using UnityEngine;

namespace View.Menu
{
    public class ScoreView : MonoBehaviour
    {
        public string RecordTime
        {
            get
            {
                var services = ServiceLocator.Current;
                var record = services.Get<IHighScoreManager>().HighScore;

                var recordMinutes = Mathf.FloorToInt(record / 60f);
                var recordSeconds = Mathf.FloorToInt(record % 60f);
                var recordMilliseconds = Mathf.FloorToInt((record - Mathf.Floor(record)) * 100f);
            
                return $"{recordMinutes:00}:{recordSeconds:00}.{recordMilliseconds:00}";
            }
        }

        public string CurrentTime
        {
            get
            {
                var services = ServiceLocator.Current;
                var time = services.Get<ITimer>().Time;
                
                var minutes = Mathf.FloorToInt(time / 60f);
                var seconds = Mathf.FloorToInt(time % 60f);
                var milliseconds = Mathf.FloorToInt((time - Mathf.Floor(time)) * 100f);

                return $"{minutes:00}:{seconds:00}.{milliseconds:00}";
            }
        }
    }
}