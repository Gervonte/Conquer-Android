using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip[] footSteps = new AudioClip[3];
    [SerializeField] private AudioClip[] attackSounds = new AudioClip[3];
    [SerializeField] private AudioClip[] deathSounds = new AudioClip[3];
    [SerializeField] private AudioClip[] hitSounds = new AudioClip[3];
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Step()
    {
        AudioClip stepSound = GetRandomClip(footSteps);
        AudioManager.Instance.PlaySFX(stepSound);
    }
    private void attackSound()
    {
        AudioClip sound = GetRandomClip(attackSounds);
        AudioManager.Instance.PlaySFX(sound);
    }
    public void death()
    {
        AudioClip deathSound = GetRandomClip(deathSounds);
        AudioManager.Instance.PlaySFX(deathSound);
    }

    public void hit()
    {
        AudioClip hitSound = GetRandomClip(hitSounds);
        AudioManager.Instance.PlaySFX(hitSound);
    }
    private AudioClip GetRandomClip(AudioClip[] source)
    {
        return source[UnityEngine.Random.Range(0, source.Length)];
    }
    // Update is called once per frame
    void Update()
    {

    }
}
