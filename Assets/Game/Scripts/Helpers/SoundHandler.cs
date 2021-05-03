using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundHandler : MonoBehaviour
{
    [SerializeField] AudioClip[] clips; // drag and add audio clips in the inspector
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ChangeTheSound(int clipIndex) // the index of the sound, 0 for first sound, 1 for the 2nd..etc
    {
        // use one desired logic
        // this will make only one sound to play without interruption
        audioSource.clip = clips[clipIndex];
        audioSource.Play();
 
        // this will make multiple sound to play with interruption
        // audioSource.PlayOneShot(clips[clipIndex]);
    }
}
 