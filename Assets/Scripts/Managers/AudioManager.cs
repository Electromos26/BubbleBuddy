    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Audio;
    using Utils;
    
    namespace Managers
    {
        [RequireComponent(typeof(AudioSource))]
        public class AudioManager : Singleton<AudioManager>
        {
            [SerializeField] private AudioMixer mixer;
    
            [SerializeField] private AudioMixerGroup masterGroup;
            [SerializeField] private AudioMixerGroup sfxGroup;
            [SerializeField] private AudioMixerGroup musicGroup;
    
            private AudioSource _audioSource;
            private readonly List<AudioSource> _availableSources = new ();
    
            private void Start()
            {
                _audioSource = GetComponent<AudioSource>();
                _availableSources.Add(_audioSource);
            }
    
            public void PlayAudioSfx(AudioClip clip)
            {
                var speaker = GetAudioSource();
                speaker.PlayOneShot(clip);
            }
    
            private AudioSource GetAudioSource()
            {
                if (_availableSources.Count > 0)
                {
                    for (var index = 0; index < _availableSources.Count; index++)
                    {
                        var source = _availableSources[index];
                        if (!source.isPlaying)
                        {
                            return source;
                        }
                    }
                }
    
                var newSource = Instantiate(_audioSource, transform);
                _availableSources.Add(newSource);
                return newSource;
            }
        }
    }