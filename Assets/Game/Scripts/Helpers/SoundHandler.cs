using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundHandler : MonoBehaviour
{
    [SerializeField] AudioClip[] clips; // drag and add audio clips in the inspector

    AudioSource audioSource;

    public bool canStackSound = false;
    public string soundType = "game";
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (soundType == "game")
        {
            audioSource.volume = GameInfo.gameSoundLevel;
        }
        if (soundType == "music")
        {
            audioSource.volume = GameInfo.musicSoundLevel;
        }
    }
    public void ChangeTheSound(int clipIndex) // the index of the sound, 0 for first sound, 1 for the 2nd..etc
    {
        if (!canStackSound)
        {
            audioSource.clip = clips[clipIndex];
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(clips[clipIndex]);
        }

    }
}
