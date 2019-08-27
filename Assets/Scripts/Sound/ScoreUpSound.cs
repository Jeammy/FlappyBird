using System;
using UnityEngine;

namespace Game
{
    public class ScoreUpSound : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private AudioSource audioSource;
        private ScoreEventChannel scoreEventChannel;

        private void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            scoreEventChannel = Finder.ScoreEventChannel;
        }

        private void OnEnable()
        {
            scoreEventChannel.OnScored += PlayScoreSound;
            
        }

        private void OnDisable()
        {
            scoreEventChannel.OnScored -= PlayScoreSound;
        }

        private void PlayScoreSound()
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}