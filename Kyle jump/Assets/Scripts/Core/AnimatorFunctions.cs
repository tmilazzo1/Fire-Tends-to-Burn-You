using UnityEngine;

[System.Serializable]
public class SoundBank
{
    public AudioClip[] variations;
    [Range(0f, 2f)]
    public float volume = 1f;
}

public class AnimatorFunctions : MonoBehaviour
{
    [SerializeField] SoundBank[] soundBank;

    public void PlaySound(int soundIndex)
    {
        AudioClip audioclip = soundBank[soundIndex].variations[Random.Range(0, soundBank[soundIndex].variations.Length)];
        float volume = soundBank[soundIndex].volume;
        GameManager.Instance.GetComponent<AudioManager>().playSoundEffect(audioclip, volume);
    }
}
