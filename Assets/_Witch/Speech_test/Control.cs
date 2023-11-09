using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    SpeechToText SpeechToText;
    SentimentAnalysis SentimentAnalysis;
    AudioRecorder AudioRecorder;
    MicrophoneInputToLight color;
    MagicController magic;
    PotControl pot;
    GameControl gc;

    bool isMicrophone, isRecording = false, isSTProcess = false, isSAProcess = false;
    string script = "";
    float score, magnitude;

    void Start()
    {
        SpeechToText = gameObject.GetComponent<SpeechToText>();
        SentimentAnalysis = gameObject.GetComponent<SentimentAnalysis>();
        AudioRecorder = gameObject.GetComponent<AudioRecorder>();
        color = gameObject.GetComponent<MicrophoneInputToLight>();
        magic = GameObject.Find("stick").GetComponent<MagicController>();
        pot = gameObject.GetComponent<PotControl>();
        gc = gameObject.GetComponent<GameControl>();

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
                
                gc.AddTutorialMagic();
                pot.MagicFinish(score);
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