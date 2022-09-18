using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioSource buttonSoundSource;
    public void playButtonSound() {
        buttonSoundSource.Stop();
        buttonSoundSource.clip = buttonSound;
        buttonSoundSource.loop = false;
        buttonSoundSource.Play();
    }
}
