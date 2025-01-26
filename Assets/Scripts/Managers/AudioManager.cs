using System;
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

        private AudioSource audioSource;
        private List<AudioSource> availableSources = new List<AudioSource>();

        private void Start()
        {
           availableSources.Add(GetComponentInChildren<AudioSource>());
        }

        public void PlayAudioSfx(AudioClip clip)
        {
            var speaker = GetAudioSource();
            speaker.PlayOneShot(clip);
        }

        private AudioSource GetAudioSource()
        {
            if (availableSources[0].isPlaying)
            {
                var newAudio = Instantiate(audioSource, Vector3.zero, Quaternion.identity);
                newAudio.transform.SetParent(transform);
                availableSources.Add(newAudio);
                return newAudio;
                
            }
            return availableSources[0];
        }
    }
}