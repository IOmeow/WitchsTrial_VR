using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public AudioSource SE;
    public AudioClip magicSE, envelopeSE;

    public void playMagigSE(){
        SE.PlayOneShot(magicSE);
    }

    public void playEnvelopeSE(){
        SE.PlayOneShot(envelopeSE);
    }

}
