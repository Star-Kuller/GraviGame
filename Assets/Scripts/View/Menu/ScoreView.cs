using System;
using Services;
using Services.ServiceLocator;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace View.Menu
{
    public class ScoreView : MonoBehaviour
    {
        public readonly StringVariable RecordTime = new StringVariable();
        public readonly StringVariable CurrentTime = new StringVariable();

        private void OnEnable()
        {
            var services = ServiceLocator.Current;
            var time = services.Get<Timer>().Time;
            var record = services.Get<HighScoreManager>().HighScore;
            
            var minutes = Mathf.FloorToInt(time / 60f);
            var seconds = Mathf.FloorToInt(time % 60f);
            var milliseconds = Mathf.FloorToInt((time - Mathf.Floor(time)) * 100f);

            CurrentTime.Value = $"{minutes:00}:{seconds:00}.{milliseconds:00}";

            var recordMinutes = Mathf.FloorToInt(record / 60f);
            var recordSeconds = Mathf.FloorToInt(record % 60f);
            var recordMilliseconds = Mathf.FloorToInt((record - Mathf.Floor(record)) * 100f);
            
            RecordTime.Value = $"{recordMinutes:00}:{recordSeconds:00}.{recordMilliseconds:00}";
        }
    }
}