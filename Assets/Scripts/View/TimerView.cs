using Services;
using Services.Interfaces;
using Services.ServiceLocator;
using TMPro;
using UnityEngine;

namespace View
{
    public class TimerView : MonoBehaviour
    {
        private ITimer _timer;
        private TextMeshProUGUI _text;
        private void Start()
        {
            var services = ServiceLocator.Current;
            _timer = services.Get<ITimer>();
            _text = GetComponent<TextMeshProUGUI>();
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