using Services.ServiceLocator;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    public class MenuView : MonoBehaviour
    {
        private void Start()
        {
            var services = ServiceLocator.Current;
            var score = transform.Find("Score");
            var scoreText = score.GetComponent<TMP_Text>();
            
            var time = services.Get<Timer>().Time;
            var minutes = Mathf.FloorToInt(time / 60f);
            var seconds = Mathf.FloorToInt(time % 60f);
            var milliseconds = Mathf.FloorToInt((time - Mathf.Floor(time)) * 100f);
            
            scoreText.text = $"Record: {minutes:00}:{seconds:00}.{milliseconds:00}\nCurrent: {minutes:00}:{seconds:00}.{milliseconds:00}";
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
    }
}