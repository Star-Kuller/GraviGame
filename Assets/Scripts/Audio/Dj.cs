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
        public string CurrentSongName() => audioClips[_currentClipIndex].name;
        public bool IsPaused { get; private set; } = false;
        
        private AudioSource[] _audioSources;
        private int _currentAudioSourceIndex = 0;
        private int _currentClipIndex = 0;
        private bool _skipped;

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
            _audioSources[0].clip = audioClips[0];
            _audioSources[0].Play();
            while (true)
            {
                var currentSource = _audioSources[_currentAudioSourceIndex];
                var nextSource = _audioSources[(_currentAudioSourceIndex + 1) % 2];

                currentSource.clip = audioClips[_currentClipIndex];
                nextSource.clip = audioClips[(_currentClipIndex + 1) % audioClips.Count];

                yield return new WaitUntil(() => currentSource.time >= currentSource.clip.length - fadeDuration || (!currentSource.isPlaying && !IsPaused) || _skipped);
                
                _currentClipIndex = (_currentClipIndex + 1) % audioClips.Count;
                nextSource.time = 0;
                nextSource.Play();
                StartCoroutine(CrossFade(currentSource, nextSource));
                _currentAudioSourceIndex = (_currentAudioSourceIndex + 1) % 2;
                
                yield return new WaitUntil(() => currentSource.isPlaying || _skipped);
                _skipped = false;
            }
        }

        private IEnumerator CrossFade(AudioSource fadeOutSource, AudioSource fadeInSource)
        {
            var timeElapsed = 0f;

            while (timeElapsed < fadeDuration)
            {
                timeElapsed += Time.deltaTime;
                var t = timeElapsed / fadeDuration;

                fadeOutSource.volume = 1 - t;
                fadeInSource.volume = t;

                yield return null;
            }

            fadeOutSource.Stop();
        }

        #region ControllFromInspector
        
        private void Update()
        {
            if (IsPaused || !_audioSources[_currentAudioSourceIndex].isPlaying) return;
            AudioProgress = _audioSources[_currentAudioSourceIndex].time / _audioSources[_currentAudioSourceIndex].clip.length;
        }

        public void SetAudioProgress(float progress)
        {
            AudioProgress = progress;
            var currentSource = _audioSources[_currentAudioSourceIndex];
            var nextSource = _audioSources[(_currentAudioSourceIndex + 1) % 2];
            currentSource.time = progress * currentSource.clip.length;
            nextSource.Stop();
            nextSource.time = 0;
        }
        
        public void TogglePause()
        {
            IsPaused = !IsPaused;
            var currentSource = _audioSources[_currentAudioSourceIndex];
            var nextSource = _audioSources[(_currentAudioSourceIndex + 1) % 2];
            if (IsPaused)
            {
                currentSource.Pause();
                if(nextSource.isPlaying)
                    nextSource.Pause();
            }
            else
            {
                currentSource.UnPause();
                if(nextSource.time > 0)
                    nextSource.UnPause();
            }
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
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField(dj.CurrentSongName());
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginChangeCheck();
            var newProgress = EditorGUILayout.Slider("Audio Progress", dj.AudioProgress, 0f, 1f);
            if (EditorGUI.EndChangeCheck())
            {
                dj.SetAudioProgress(newProgress);
            }

            if (GUILayout.Button(dj.IsPaused ? "Resume" : "Pause"))
            {
                dj.TogglePause();
            }
            EditorGUILayout.EndVertical();
            
            if (Application.isPlaying)
            {
                Repaint();
            }
        }
    }
}
