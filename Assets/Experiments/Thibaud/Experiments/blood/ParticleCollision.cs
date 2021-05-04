using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    ParticleSystem particle;
    public GameObject splatPrefab;
    public Transform splatHolder;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public AudioSource audioSource;
    public AudioClip[] sounds;
    public float SoundCapResetSpeed = 0.55f;
    public int MaxSounds = 3;
    float TimePassed;
    int soundsPlayed;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        TimePassed += Time.deltaTime;
        if (TimePassed > SoundCapResetSpeed)
        {
            soundsPlayed = 0;
            TimePassed = 0;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particle, other, collisionEvents);

        int count = collisionEvents.Count;

        for (int i = 0; i < count; i++)
        {
            Instantiate(splatPrefab, collisionEvents[i].intersection, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)), splatHolder);
            if (soundsPlayed < MaxSounds)
            {
                soundsPlayed += 1;
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)], Random.Range(0.1f, 0.35f));
            }
        }
    }
}