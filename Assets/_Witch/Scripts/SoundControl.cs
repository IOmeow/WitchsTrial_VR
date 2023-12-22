using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl instance { get; private set; }
    public AudioSource SE, BGM;
    public AudioClip magicSE, envelopeSE, potSE, flySE;
    public AudioClip bellSE, doorSE, chalkSE, frameSE, frameReverseSE;
    public List<AudioClip> output = new List<AudioClip>();
    public List<AudioClip> bgm = new List<AudioClip>();

    void Start(){
        instance = this;
    }
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

    private void playFlySE(){
        SE.PlayOneShot(flySE);
    }

    public void playBGM(int scene){
        BGM.Pause();
        Invoke("playFlySE", 3f);

        BGM.clip = bgm[scene-1];
        Invoke("_playBGM", 5f);
        Debug.Log("bgm"+scene);
    }
    void _playBGM(){
        BGM.Play();
    }

    public void playBellSE(){
        SE.PlayOneShot(bellSE);
    }

    public void playDoorSE(){
        SE.PlayOneShot(doorSE);
    }
    public void playChalkSE(){
        SE.PlayOneShot(chalkSE);
    }
    public void playFrameSE(bool isGood){
        if(isGood)SE.PlayOneShot(frameReverseSE);
        else SE.PlayOneShot(frameSE);
    }

    private static IEnumerator FadeMusic(AudioSource audioSource, float duration, float targetVolume){
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}
