using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    [JsonProperty("musicVolume")] private float _musicVolume = 1;
    [JsonProperty("effectsVolume")] private float _effectsVolume = 1;

    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = Mathf.Clamp(value, 0.0001f, 1);
        }
    }

    public float EffectsVolume
    {
        get => _effectsVolume;
        set
        {
            _effectsVolume = Mathf.Clamp(value, 0.0001f, 1);
        }
    }

}
