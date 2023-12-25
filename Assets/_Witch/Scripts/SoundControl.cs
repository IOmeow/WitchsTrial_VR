using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl instance { get; private set; }
    public AudioSource SE, BGM;
    public AudioClip magicSE, envelopeSE, potSE, flySE;
    public AudioClip projectionSE, bellSE, doorSE, chalkSE, frameSE, frameReverseSE, fileSE, fileReverseSE;
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
        StartCoroutine(FadeOutAndInBGM(scene));
    }
    IEnumerator FadeOutAndInBGM(int scene)
    {
        while (BGM.volume > 0)
        {
            BGM.volume -= Time.deltaTime / 2; // 調整淡出速度，可以根據需求調整
            yield return null;
        }

        // 暫停並更換音樂
        BGM.Pause();
        BGM.clip = bgm[scene];

        // 淡入
        while (BGM.volume < 0.2)
        {
            BGM.volume += Time.deltaTime / 2; // 調整淡入速度，可以根據需求調整
            yield return null;
        }

        // 播放音樂
        BGM.Play();
        Debug.Log("BGM switched to scene " + scene);
    }

    public void playProjectionSE(){
        SE.PlayOneShot(projectionSE);
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
    public void playFileSE(bool isGood){
        if(isGood)SE.PlayOneShot(fileReverseSE);
        else SE.PlayOneShot(fileSE);
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
