using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public AudioSource SE;
    public AudioClip magicSE, envelopeSE, potSE;

    public void playMagigSE(){
        SE.PlayOneShot(magicSE);
    }

    public void playEnvelopeSE(){
        SE.PlayOneShot(envelopeSE);
    }

    public void playPotSE(){
        SE.PlayOneShot(potSE);
    }

}
