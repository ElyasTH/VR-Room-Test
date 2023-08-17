using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
    }
    
    public List<SoundAudioClip> soundAudioClipsList = new List<SoundAudioClip>();
    
    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.SoundType soundType;
        public AudioClip audioClip;
        
        [SerializeField, Range(0f,1f)]
        public float volume = 1f;
        [SerializeField, Range(0f,1f)]
        public float pitch = 1f;
        
        [Header("Advanced Settings")]
        public float spatialBlend = 1f;
        public float maxDistance = 20f;
        public AudioRolloffMode rolloffMode = AudioRolloffMode.Linear;
        public float dopplerLevel;

        public SoundManager.SoundSettings GetDefaultSettings()
        {
            return new SoundManager.SoundSettings(volume, pitch, spatialBlend, maxDistance, rolloffMode, dopplerLevel);;
        }
    }
}
