using System;
using Services;
using Services.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class TimerView : MonoBehaviour
    {
        private Timer _timer;
        private Text _text;
        private void Start()
        {
            var services = ServiceLocator.Current;
            _timer = services.Get<Timer>();
            _text = GetComponent<Text>();
        }

        public void Update()
        {
            var time = _timer.Time;
            var minutes = Mathf.FloorToInt(time / 60f);
            var seconds = Mathf.FloorToInt(time % 60f);
            var milliseconds = Mathf.FloorToInt((time - Mathf.Floor(time)) * 100f);
        
            _text.text = $"{minutes:00}:{seconds:00}.{milliseconds:00}";
        }
    }
}