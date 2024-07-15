using Services;
using Services.ServiceLocator;
using UnityEngine;

namespace View.Menu
{
    public class ScoreLineView : MonoBehaviour
    {
        [SerializeField] private RectTransform scoreIndicator;
        private void OnEnable()
        {
            var services = ServiceLocator.Current;

            var time = services.Get<Timer>().Time;
            var record = services.Get<HighScoreManager>().HighScore;
            
            var scoreLineWidth = transform.GetComponent<RectTransform>().rect.width;
            var right = scoreLineWidth - (Mathf.Clamp(record / time, 0, 1) * scoreLineWidth);
            scoreIndicator.offsetMax = new Vector2(-right, scoreIndicator.offsetMax.y);
        }
    }
}