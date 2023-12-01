using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SpeechControl : MonoBehaviour
{
    SpeechToText SpeechToText;
    SentimentAnalysis SentimentAnalysis;
    AudioRecorder AudioRecorder;
    MagicController magic;
    SoundControl sound;
    VolumeChange volume;

    bool isMicrophone, isRecording = false, isSTProcess = false, isSAProcess = false;
    string script = "";
    float score, magnitude;

    void Start()
    {
        SpeechToText = gameObject.GetComponent<SpeechToText>();
        SentimentAnalysis = gameObject.GetComponent<SentimentAnalysis>();
        AudioRecorder = gameObject.GetComponent<AudioRecorder>();
        magic = GameObject.Find("stick").GetComponent<MagicController>();
        sound = GameObject.Find("SoundControl").GetComponent<SoundControl>();
        volume = GameObject.Find("volume_hint").GetComponent<VolumeChange>();

        isMicrophone = AudioRecorder.CheckMicrophone();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.R)){
            startRecord();
        }
        if(Input.GetKeyDown(KeyCode.E)){
            stopRecord();
        }

        if(isSTProcess){
            script = SpeechToText.GetTranscript();
            if(script == "ledTime"){
                isSTProcess = false;
                Debug.Log("Analyze Canceled");

                magic.StopGlowColor();
                ResetRecord();
                magic.startHint();
                volume.stopVolume();
            }
            else if(script != "ERROR"){
                isSTProcess = false;
                Debug.Log("Analyze: "+script);
                SentimentAnalysis.AnalyzeSentiment(script);
                isSAProcess = true;
            }
        }
        if(isSAProcess){
            score = SentimentAnalysis.GetScore();
            magnitude = SentimentAnalysis.GetMagnitude();
            if(score != -2f && magnitude != -2f){
                isSAProcess = false;
                magic.StopGlowColor();
                magic.ChangeGlowColor(score, magnitude);
                Invoke("ResetRecord", 10);
                
                sound.playMagigFinishSE(score);
                volume.endVolume();
                GameManager.instance.endMagic();
            }
        }
    }

    public void startRecord(){
        if(!isRecording){
            AudioRecorder.StartRecording();
            isRecording = true;
        }
        
    }
    public void stopRecord(){
        if(isRecording){
            AudioRecorder.StopRecording();
            isRecording = false;

            SpeechToText.Recognize();
            isSTProcess = true;
        }
    }

    void ResetRecord(){
        magic.ResetGlow();
    }
}