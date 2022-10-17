using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Unity Setup")]

    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioMixer audioMixer;

    [Header("Shoot Sound")]

    [SerializeField] AudioClip shootSound;
    [Range(0f, 1.1f)]
    [SerializeField] float shootVolume;
    float counter;

    public void playSoundEffect(AudioClip audioClip, float volume)
    {
        sfxAudioSource.PlayOneShot(audioClip, volume);
    }

    private void FixedUpdate()
    {
        counter += Time.deltaTime;
    }

    public void playShootSound()
    {
        if(counter > 1f)
        {
            counter = 0;
            playSoundEffect(shootSound, shootVolume);
        }
    }

    public void setMusicVolume(float newVolume)
    {
        audioMixer.SetFloat("musicVolume", formatVolume(newVolume));
    }

    public float getMusicVolume()
    {
        audioMixer.GetFloat("musicVolume", out float volume);
        return unFormatVolume(volume);
    }

    public void setSFXVolume(float newVolume)
    {
        audioMixer.SetFloat("sfxVolume", formatVolume(newVolume));
    }

    public float getSFXVolume()
    {
        audioMixer.GetFloat("sfxVolume", out float volume);
        return unFormatVolume(volume);
    }

    float formatVolume(float input)
    {
        if(input < 0)
        {
            input = 0;
        }else if(input > 10)
        {
            input = 10;
        }

        //ranges from -20 to 20
        float output = 4 * (input - 5);
        if (output <= -20) output = -80;

        return output;
    }

    float unFormatVolume(float input)
    {
        if(input < -20)
        {
            return 0;
        }else
        {
            return input / 4 + 5;
        }
    }
}
