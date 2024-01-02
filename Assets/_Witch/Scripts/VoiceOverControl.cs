using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverControl : MonoBehaviour
{
    public static VoiceOverControl instance { get; private set; }
    private AudioSource voice;
    [SerializeField]private AudioSource slide;
    public List<AudioClip> tutorial = new List<AudioClip>();
    public List<AudioClip> murmur = new List<AudioClip>();
    public List<AudioClip> trial = new List<AudioClip>();
    public List<AudioClip> story1 = new List<AudioClip>();
    public List<AudioClip> story2 = new List<AudioClip>();

    int mur_index=0, tutorial_index, trial_index;
    public bool finshAudio = true;

    void Start()
    {
        instance = this;
        voice = this.GetComponent<AudioSource>();
    }
    
    public void playTutorial(int index, bool recall){
        if (voice.isPlaying) voice.Pause();
        voice.clip = tutorial[index];
        voice.Play();

        // voice.PlayOneShot(tutorial[index]);
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
        case 6:
            GameManager.instance.canChangeTrial1Page();
            break;
        default:
            break;
        }
    }
    void delayToPlayTutorial(){
        playTutorial(1, true);
    }

    public void playTrial(int index, bool recall){
        if(index==0||index==1)slide.PlayOneShot(trial[index]);
        else voice.PlayOneShot(trial[index]);
        trial_index = index;
        if(recall)Invoke("OnTrialAudioFinished", trial[index].length);
    }
    void OnTrialAudioFinished()
    {
        // Debug.Log("音檔播放完畢！");
        switch(trial_index){
        case 0:
            playTrial(2,true);
            break;
        case 1:
            playTrial(2,true);
            break;
        case 2:
            GameManager.instance.canStartMagic();
            break;
        case 5:
        case 6:
        case 7:
            // 鐘聲
            Invoke("playEnding", 3f);
            break;
        case 8:
            // End game

            break;
        default:
            break;
        }
    }
    void playEnding(){
        SoundControl.instance.playBellSE();
        playTrial(8, true);
    }
    public void playStory1(int index){
        finshAudio=false;
        slide.PlayOneShot(story1[index]);
        Invoke("OnStoryAudioFinished", story1[index].length);
    }
    public void playStory2(int index){
        finshAudio=false;
        slide.PlayOneShot(story2[index]);
        Invoke("OnStoryAudioFinished", story2[index].length);
    }
    void OnStoryAudioFinished(){
        // 播完
        finshAudio=true;
    }
}