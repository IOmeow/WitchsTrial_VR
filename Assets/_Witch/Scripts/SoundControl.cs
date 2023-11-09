using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public AudioSource SE;
    public AudioClip magicSE, envelopeSE, potSE;
    public List<AudioClip> output = new List<AudioClip>();

    public void playMagigSE(){
        SE.PlayOneShot(magicSE);
    }

    public void playMagigFinishSE(float score){
        if(score<-0.7f)SE.PlayOneShot(output[0]);
        else if(score<-0.5f)SE.PlayOneShot(output[1]);
        else if(score<-0.3f)SE.PlayOneShot(output[2]);
        else if(score<0f)SE.PlayOneShot(output[3]);
        else if(score<0.3f)SE.PlayOneShot(output[4]);
        else if(score<0.5f)SE.PlayOneShot(output[5]);
        else if(score<0.7f)SE.PlayOneShot(output[6]);
        else SE.PlayOneShot(output[7]);
    }

    public void playEnvelopeSE(){
        SE.PlayOneShot(envelopeSE);
    }

    public void playPotSE(){
        SE.PlayOneShot(potSE);
    }

}
