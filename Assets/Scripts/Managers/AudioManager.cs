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

      public void PlayAudioSfx(AudioClip clip)
      {
      
      }
   }
}
