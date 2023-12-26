using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverControl : MonoBehaviour
{
    public static VoiceOverControl instance { get; private set; }
    private AudioSource voice;
    public List<AudioClip> tutorial = new List<AudioClip>();
    public List<AudioClip> murmur = new List<AudioClip>();
    public List<AudioClip> trial = new List<AudioClip>();

    int mur_index=0, tutorial_index, trial_index;

    void Start()
    {
        instance = this;
        voice = this.GetComponent<AudioSource>();
    }
    
    public void playTutorial(int index, bool recall){
        voice.PlayOneShot(tutorial[index]);
        tutorial_index = index;
        if(recall)Invoke("OnTutorialAudioFinished", tutorial[index].length);
    }

    public void startMurmur(){
        InvokeRepeating("playMurmur", 25f, 10f);
    }
    public void stopMurmur(){
        CancelInvoke("playMurmur");
    }
    private void playMurmur(){
        voice.PlayOneShot(murmur[mur_index]);
        mur_index++;
        mur_index %= murmur.Count;
    }

    void OnTutorialAudioFinished()
    {
        Debug.Log("音檔播放完畢！");
        switch(tutorial_index){
        case 0:
            Invoke("delayToPlayTutorial", 0.1f);
            break;
        case 1:
            // 魔杖出現
            GameManager.instance.ActiveMagicStick();
            break;
        case 2:
            GameManager.instance.canStartMagic();
            break;
        case 5:
            // 黑板浮現情緒句子
            GameManager.instance.ShowEmotionWord();
            break;
        default:
            break;
        }
    }
    void delayToPlayTutorial(){
        playTutorial(1, true);
    }

    public void playTrial(int index, bool recall){
        voice.PlayOneShot(trial[index]);
        trial_index = index;
        if(recall)Invoke("OnTrialAudioFinished", trial[index].length);
    }
    void OnTrialAudioFinished()
    {
        // Debug.Log("音檔播放完畢！");
        switch(trial_index){
        case 0:
            playTrial(2,false);
            break;
        case 1:
            playTrial(2,false);
            break;
        case 5:
        case 6:
        case 7:
            // 鐘聲
            SoundControl.instance.playBellSE();
            playTrial(8, true);
            break;
        case 8:
            // End game

            break;
        default:
            break;
        }
    }

}