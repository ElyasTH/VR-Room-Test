using Sirenix.OdinInspector;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    #region Variables

    public SoundManager.SoundType soundType;
    public SoundManager.SettingsType settingsType;
    public SoundManager.SoundSettings soundSettings1;

    [ShowIf("settingsType", SoundManager.SettingsType.Custom)]
    [Header("Basic Settings")]
    [Range(0f,1f)]
    public float volume = 1f;
    [ShowIf("settingsType", SoundManager.SettingsType.Custom)]
    [Range(0f,1f)]
    public float pitch = 1f;
    
    [ShowIf("settingsType", SoundManager.SettingsType.Custom)]
    [Header("Advanced Settings")]
    public float spatialBlend = 1f;
    [ShowIf("settingsType", SoundManager.SettingsType.Custom)]
    public float maxDistance = 20f;
    [ShowIf("settingsType", SoundManager.SettingsType.Custom)]
    public AudioRolloffMode rolloffMode = AudioRolloffMode.Linear;
    [ShowIf("settingsType", SoundManager.SettingsType.Custom)]
    public float dopplerLevel;
    
    #endregion
    public void Play()
    {
        switch (settingsType)
        {
            case SoundManager.SettingsType.Default:
            {
                SoundManager.PlaySound(soundType, gameObject);
                break;
            }

            case SoundManager.SettingsType.Custom:
            {
                SoundManager.SoundSettings soundSettings =
                    new SoundManager.SoundSettings(volume, pitch, spatialBlend, maxDistance, rolloffMode, dopplerLevel);
                SoundManager.PlaySound(soundType, gameObject, soundSettings);
                break;
            }
        }
    }
    
    
}
