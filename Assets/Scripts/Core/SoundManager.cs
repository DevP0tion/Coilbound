using UnityEngine;
using UnityEngine.Audio;

namespace Coilbound.Core
{
  public class SoundManager : MonoBehaviour
  {
    [SerializeField] private AudioMixer audioMixer;

    public float MasterVolume
    {
      get => audioMixer.GetFloat("Master", out var value) ? value + 80 : PlayerPrefs.GetFloat("MasterVolume", 0.0f);
      set
      {
        audioMixer.SetFloat("Master", value - 80);
        PlayerPrefs.SetFloat("MasterVolume", value);
      }
    }

    public float BGMVolume
    {
      get => audioMixer.GetFloat("BGM", out var value) ? value + 80 : PlayerPrefs.GetFloat("BGMVolume", 0.0f);
      set
      {
        audioMixer.SetFloat("BGM", value - 80);
        PlayerPrefs.SetFloat("BGMVolume", value);
      }
    }

    public float SFXVolume
    {
      get => audioMixer.GetFloat("SFX", out var value) ? value + 80 : PlayerPrefs.GetFloat("SFXVolume", 0.0f);
      set
      {
        audioMixer.SetFloat("SFX", value - 80);
        PlayerPrefs.SetFloat("SFXVolume", value);
      }
    }

    public void LoadSoundOption()
    {
      MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.0f);
      BGMVolume = PlayerPrefs.GetFloat("BGMVolume", 0.0f);
      SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.0f);
    }
  }
}