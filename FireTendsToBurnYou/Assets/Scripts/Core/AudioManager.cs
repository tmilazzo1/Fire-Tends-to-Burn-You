using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Unity Setup")]

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject audioManagerParent;

    [Header("SFX")]

    [SerializeField] AudioSource sfxAudioSource;

    [Header("Music")]

    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] GameObject musicSourcePrefab;
    AudioSource currentMusicSource;

    [Header("Shoot Sound")]

    [SerializeField] AudioClip shootSound;
    [Range(0f, 1.1f)]
    [SerializeField] float shootVolume;
    float shootSoundCounter;

    [Header("Fireball Sound")]

    [SerializeField] AudioClip fireballSound;
    [Range(0f, 1.1f)]
    [SerializeField] float fireballVolume;
    [SerializeField] float soundMultiplierIncrement;
    float soundMultiplier = 1;

    private void Start()
    {
        currentMusicSource = musicAudioSource;
    }

    public void playSoundEffect(AudioClip audioClip, float volume)
    {
        sfxAudioSource.PlayOneShot(audioClip, volume);
    }

    private void FixedUpdate()
    {
        shootSoundCounter += Time.deltaTime;
        if(soundMultiplier < 1) soundMultiplier += Time.deltaTime * 3;
    }

    public void playShootSound()
    {
        if(shootSoundCounter > 1f)
        {
            shootSoundCounter = 0;
            playSoundEffect(shootSound, shootVolume);
        }
    }

    public void playFireballSound()
    {
        if (soundMultiplier > 0.1f) soundMultiplier -= soundMultiplierIncrement;
        playSoundEffect(fireballSound, soundMultiplier * fireballVolume);
    }

    public IEnumerator changeSong(float lerpTime, AudioClip newClip)
    {
        float counter = 0;
        float newVolume = 0;
        GameObject tempMusicObject = Instantiate(musicSourcePrefab, audioManagerParent.transform);
        AudioSource tempMusicSource = tempMusicObject.GetComponent<AudioSource>();
        tempMusicSource.clip = newClip;
        tempMusicSource.Play();

        while(newVolume < 1)
        {
            counter += Time.unscaledDeltaTime / lerpTime;
            newVolume = Mathf.Lerp(0, 1, counter);
            currentMusicSource.volume = 1 - newVolume;
            tempMusicSource.volume = newVolume;
            yield return new WaitForSeconds(0);
        }

        Destroy(currentMusicSource.gameObject);
        currentMusicSource = tempMusicSource;
        currentMusicSource.gameObject.name = "currentMusicSource";
    }

    public IEnumerator fadeSongOut(float lerpTime)
    {
        float counter = 0;
        float originalVolume = currentMusicSource.volume;
        float volume = originalVolume;

        while (volume > 0)
        {
            counter += Time.unscaledDeltaTime / lerpTime;
            volume = Mathf.Lerp(originalVolume, 0, counter);
            currentMusicSource.volume = volume;
            yield return new WaitForSeconds(0);
        }
    }

    public IEnumerator fadeSongIn(float lerpTime, AudioClip newClip)
    {
        float counter = 0;
        float originalVolume = currentMusicSource.volume;
        float volume = originalVolume;
        if(newClip) currentMusicSource.clip = newClip;
        currentMusicSource.Play();

        while (volume < 1)
        {
            counter += Time.unscaledDeltaTime / lerpTime;
            volume = Mathf.Lerp(originalVolume, 1, counter);
            currentMusicSource.volume = volume;
            yield return new WaitForSeconds(0);
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
