using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class Dj : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> audioClips;
        [SerializeField] private float fadeDuration = 2f;
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        
        public float AudioProgress { get; private set; } = 0f;
        public string CurrentSongName => audioClips[_currentClipIndex].name;
        public bool IsPaused { get; private set; } = false;
        
        private AudioSource[] _audioSources;
        private int _currentAudioSourceIndex = 0;
        private int _currentClipIndex = 0;
        private int NextSourceIndex => (_currentAudioSourceIndex + 1) % 2;
        private int NextClipIndex => (_currentClipIndex + 1) % audioClips.Count;
        
        private bool _isApplicationPaused = false;

        private void OnApplicationPause(bool pauseStatus)
        {
            _isApplicationPaused = pauseStatus;
            HandleApplicationStateChange();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            _isApplicationPaused = !hasFocus;
            HandleApplicationStateChange();
        }

        private void HandleApplicationStateChange()
        {
            if(_audioSources is not { Length: 2 }) return;
            
            if (_isApplicationPaused)
            {
                foreach (var source in _audioSources)
                {
                    if (source != null && source.isPlaying)
                        source.Pause();
                }
            }
            else
            {
                foreach (var source in _audioSources)
                {
                    if (source != null && !source.isPlaying)
                        source.UnPause();
                }
            }
        }
        
        
        private void Start()
        {
            _audioSources = new AudioSource[2];
            for (var i = 0; i < 2; i++)
            {
                _audioSources[i] = gameObject.AddComponent<AudioSource>();
                _audioSources[i].loop = false;
                _audioSources[i].outputAudioMixerGroup = audioMixerGroup;
            }
            
            if (audioClips.Count > 0)
            {
                StartCoroutine(PlayAudioSequence());
            }
        }

        private IEnumerator PlayAudioSequence()
        {
            while (true)
            {
                var currentSource = _audioSources[_currentAudioSourceIndex];
                var nextSource = _audioSources[NextSourceIndex];
                currentSource.clip = audioClips[_currentClipIndex];
                nextSource.clip = audioClips[NextClipIndex];

                if(!currentSource.isPlaying && !nextSource.isPlaying && !IsPaused && !_isApplicationPaused)
                    currentSource.Play();

                StartCoroutine(PlayAudioWithFade(currentSource, nextSource));
                
                yield return new WaitUntil(() => !currentSource.isPlaying && !IsPaused && !_isApplicationPaused);
                _currentAudioSourceIndex = NextSourceIndex;
                _currentClipIndex = NextClipIndex;
            }
        }

        private IEnumerator PlayAudioWithFade(AudioSource current, AudioSource next)
        {
            while (current.isPlaying || IsPaused || _isApplicationPaused)
            {
                if (current.time >= current.clip.length - fadeDuration)
                {
                    if(!next.isPlaying && !IsPaused && !_isApplicationPaused) next.Play();
                    var fade = (current.clip.length - current.time) / fadeDuration;
                    current.volume = fade;
                    next.volume = 1 - fade;

                    if (current.time >= current.clip.length - 0.01f)
                    {
                        current.time = 0.01f;
                        current.Stop();
                    }
                }
                else
                {
                    current.volume = 1;
                    next.volume = 0;
                    next.time = 0.01f;
                }
                yield return null;
            }
        }
        

        #region ControlFromInspector
        
        private void Update()
        {
            AudioProgress = _audioSources[_currentAudioSourceIndex].time / _audioSources[_currentAudioSourceIndex].clip.length;
        }

        public void SetAudioProgress(float progress)
        {
            var currentSource = _audioSources[_currentAudioSourceIndex];
            var nextSource = _audioSources[NextSourceIndex];
            var newTime = progress * currentSource.clip.length;
            
            if (newTime < currentSource.clip.length)
                currentSource.time = newTime;
            
            if (!(currentSource.time >= currentSource.clip.length - fadeDuration)) return;
            
            var newNextTime = currentSource.clip.length - currentSource.time;
            if (newNextTime < nextSource.clip.length)
                nextSource.time = newNextTime;
            
            nextSource.Play();
        }
        
        public void TogglePause()
        {
            IsPaused = !IsPaused;
            var currentSource = _audioSources[_currentAudioSourceIndex];
            var nextSource = _audioSources[NextSourceIndex];
            if (IsPaused)
            {
                currentSource.Pause();
                nextSource.Pause();
            }
            else
            {
                currentSource.UnPause();
                nextSource.UnPause();
            }
        }

        public void Next()
        {
            IsPaused = false;
            var currentSource = _audioSources[_currentAudioSourceIndex];
            var nextSource = _audioSources[NextSourceIndex];
            currentSource.Stop();
            currentSource.time = 0.01f;
            nextSource.time = 0.0f;
        }
        
        
        #endregion
    }
    
    [CustomEditor(typeof(Dj))]
    public class DjEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var dj = (Dj)target;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300));
            
            var songStyle = new GUIStyle(EditorStyles.textField)
            {
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white, background = Texture2D.blackTexture },
                fontSize = 14
            };
            
            GUILayout.Space(10);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label(dj.CurrentSongName, songStyle, GUILayout.Height(40));
            EditorGUILayout.EndVertical();

            GUILayout.Space(10);

            EditorGUI.BeginChangeCheck();
            var newProgress = EditorGUILayout.Slider(dj.AudioProgress, 0f, 0.98f);
            if (EditorGUI.EndChangeCheck())
            {
                dj.SetAudioProgress(newProgress);
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(dj.IsPaused ? "Resume" : "Pause", GUILayout.Width(80)))
            {
                dj.TogglePause();
            }
            if (GUILayout.Button("Next", GUILayout.Width(80)))
            {
                dj.Next();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();

            if (Application.isPlaying)
            {
                Repaint();
            }
        }
    }
}
