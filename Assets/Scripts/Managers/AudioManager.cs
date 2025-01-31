using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utils;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioMixerGroup masterGroup;
        [SerializeField] private AudioMixerGroup musicGroup;
        [SerializeField] private AudioMixerGroup sfxGroup;
        
        private List<AudioSource> sfxSources = new List<AudioSource>();

        private void Awake()
        {

            for (int i = 0; i < 10; i++) // Pre-create 10 audio sources for SFX
            {
                var sfxSource = new GameObject{name = "SFX Source"}.AddComponent<AudioSource>();
                sfxSource.outputAudioMixerGroup = sfxGroup;
                sfxSources.Add(sfxSource);
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.LogWarning("Music clip is missing.");
                return;
            }

           // musicSource.clip = clip;
           // musicSource.Play();
        }

        public void StopMusic()
        {
           // musicSource.Stop();
           // musicSource.clip = null;
        }

        public void PlayAudioSfx(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.LogWarning("SFX clip is missing.");
                return;
            }

            var sfxSource = sfxSources.Find(source => !source.isPlaying);
            if (sfxSource == null)
            {
                Debug.LogWarning("No available SFX audio source.");
                return;
            }

            sfxSource.PlayOneShot(clip);
        }
        
    }
}