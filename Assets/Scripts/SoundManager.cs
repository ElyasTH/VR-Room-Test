using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class SoundManager
{
    public enum SoundType
    {
        ActionDone,
        SnapTool,
        ButtonHover,
        ButtonClick,
        AirCompressor,
        ToolDrop,
        BoxWrench,
        LiftingMachine,
        RegulatorPartDrop,
        Ambient,
        Fan
    }

    public enum SettingsType
    {
        Default,
        Custom
    }
    public class SoundSettings
    {
        public float volume;
        public float pitch;
        public float spatialBlend;
        public float maxDistance;
        public AudioRolloffMode rolloffMode;
        public float dopplerLevel;

        public SoundSettings(float volume, float pitch, float spatialBlend, float maxDistance, AudioRolloffMode rolloffMode, float dopplerLevel)
        {
            this.volume = volume;
            this.pitch = pitch;
            this.spatialBlend = spatialBlend;
            this.maxDistance = maxDistance;
            this.rolloffMode = rolloffMode;
            this.dopplerLevel = dopplerLevel;
        }
    }

    
    private static GameObject _oneShotGameObject;
    private static AudioSource _oneShotAudioSource;
    private static Dictionary<GameObject, GameObject> _soundObjectPool = new Dictionary<GameObject, GameObject>();
    
    
    public static void PlaySound(SoundType soundType, GameObject root, SoundSettings newSettings = null)
    {
        AudioSource audioSource = FindAudioSource(soundType, root);
        GameObject soundGameObject = audioSource.gameObject;
        if (newSettings != null) ApplyNewSettings(audioSource, newSettings);
        soundGameObject.SetActive(true);
        soundGameObject.transform.position = root.transform.position;
        audioSource.Play();
        DisableAfterDelay(soundGameObject, audioSource.clip.length);
    }
    
    public static void PlaySound(SoundType soundType)
    {
        if (_oneShotGameObject == null)
        {
            _oneShotGameObject = new GameObject("One Shot Sound");
            _oneShotAudioSource = _oneShotGameObject.AddComponent<AudioSource>();
        }
        _oneShotAudioSource.PlayOneShot(GetSoundAudioClip(soundType).audioClip);
    }
    
    private static void ApplyNewSettings(AudioSource audioSource, SoundSettings newSettings)
    {
        audioSource.volume = newSettings.volume;
        audioSource.pitch = newSettings.pitch;
        audioSource.spatialBlend = newSettings.spatialBlend;
        audioSource.maxDistance = newSettings.maxDistance;
        audioSource.rolloffMode = newSettings.rolloffMode;
        audioSource.dopplerLevel = newSettings.dopplerLevel;
    }

    private static AudioSource FindAudioSource(SoundType soundType, GameObject root)
    {
        AudioSource audioSource;
        SoundController.SoundAudioClip soundAudioClip = GetSoundAudioClip(soundType);

        if (!_soundObjectPool.TryGetValue(root, out var soundGameObject))
        {
            soundGameObject = new GameObject(root.name + " Sound");
            audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = soundAudioClip.audioClip;
            _soundObjectPool.Add(root, soundGameObject);
        }
        else
        {
            audioSource = soundGameObject.GetComponent<AudioSource>();
        }
        ApplyNewSettings(audioSource, soundAudioClip.GetDefaultSettings());

        return audioSource;
    }

    private static SoundController.SoundAudioClip GetSoundAudioClip(SoundType soundType)
    {
        foreach (var soundAudioClip in SoundController.instance.soundAudioClipsList)
        {
            if (soundAudioClip.soundType == soundType)
                return soundAudioClip;
        }
        Debug.LogError("Sound " + soundType + " not Found");
        return null;
    }

    public static void AddButtonSounds(this Button button)
    {
        button.onClick.AddListener( () =>
        {
            SoundManager.PlaySound(SoundType.ButtonClick);
        });
    }
    
    private static async void DisableAfterDelay(GameObject gameObject, float delay)
    {
        await Task.Delay((int) (delay*1000));
        gameObject.SetActive(false);
    }

}
