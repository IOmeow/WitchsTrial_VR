using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    SpeechControl speech;
    // MagicRotate magicHint;

    private GameObject trail;
    private ParticleSystem glow;

    GameObject hint, track, target;
    SoundControl sound;
    VolumeChange volume;
    GameObject black_board;
    // Start is called before the first frame update
    void Awake()
    {
        speech = GameObject.Find("SpeechControl").GetComponent<SpeechControl>();
        sound = GameObject.Find("SoundControl").GetComponent<SoundControl>();
        
        glow = GameObject.Find("glow").GetComponent<ParticleSystem>();
        trail = GameObject.Find("trail");
        trail.SetActive(false);

        hint = GameObject.Find("TrackHint");
        track = GameObject.Find("track1");
        hint.SetActive(false);
        track.SetActive(false);

        target = GameObject.Find("StayTarget");

        volume = GameObject.Find("volume_hint").GetComponent<VolumeChange>();
    }

    void Start(){
        black_board = GameObject.Find("black_board_context");
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.M))startHint();
        // if(Input.GetKeyDown(KeyCode.N))stopHint();

        // if(Input.GetKeyDown(KeyCode.Z))ChangeGlowColor(-0.9f,0);
        // if(Input.GetKeyDown(KeyCode.X))ChangeGlowColor(-0.7f,0);
        // if(Input.GetKeyDown(KeyCode.C))ChangeGlowColor(-0.5f,0);
        // if(Input.GetKeyDown(KeyCode.V))ChangeGlowColor(-0.3f,0);
        // if(Input.GetKeyDown(KeyCode.B))ChangeGlowColor(-0.1f,0);

        // if(Input.GetKeyDown(KeyCode.A))ChangeGlowColor(0.9f,0);
        // if(Input.GetKeyDown(KeyCode.S))ChangeGlowColor(0.7f,0);
        // if(Input.GetKeyDown(KeyCode.D))ChangeGlowColor(0.5f,0);
        // if(Input.GetKeyDown(KeyCode.F))ChangeGlowColor(0.3f,0);
        // if(Input.GetKeyDown(KeyCode.G))ChangeGlowColor(0.1f,0);
    }
    
    int t = 0;
    bool MagicStart = false, glowEnd = true;
    void OnTriggerStay(Collider other)
    {
        if(other.name == "track1" && t==0 && glowEnd){
            t=1;
            Invoke("ResetTrack", 10);
            Debug.Log("track1");
            trail.SetActive(true);
            hint.SetActive(false);
        }
        // if(other.name == "track2" && t==1){
        //     t=2;
        //     Debug.Log("track2");
        // }
        // if(other.name == "track3" && t==2){
        //     t=3;
        //     Debug.Log("track3");
        // }
        // if(other.name == "track4" && t==3){
        //     t=4;
        //     Debug.Log("track4");
        // }
        if(other.name == "StayTarget" && t==1 && !MagicStart){
            // 倒數3秒後startMagic
            VoiceOverControl.instance.stopMurmur();
            Invoke("startMagic", 3f);
            volume.startVolume();
            t=2;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name == "StayTarget"){
            if(!MagicStart){
                // 停止動畫與倒數
                CancelInvoke("startMagic");
                volume.stopVolume();
            }
            else {
                MagicStart=false;
                glowEnd = false;
                speech.stopRecord();

                InvokeRepeating("RandomGlowColor", 0.1f, 1f);
            }
            CancelInvoke("ResetTrack");
            ResetTrack();
            target.transform.localScale = new Vector3(4f,10f,4f);
        }
    }
    void ResetTrack(){
        t=0;
        // Debug.Log("Reset track");
        trail.SetActive(false);
    }
    void startMagic(){
        MagicStart = true;
        speech.startRecord();
        if(GameManager.instance.tutorial){
            VoiceOverControl.instance.playTutorial(3, false);
            black_board.transform.GetChild(3).gameObject.SetActive(false);
            black_board.transform.GetChild(4).gameObject.SetActive(true);
        }

        glow.Play();
        sound.playMagigSE();

        stopHint();
        target.transform.localScale = new Vector3(4f,15f,4f);
    }

    void RandomGlowColor(){
        glow.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    // Color color_low = Color.cyan;
    Color color_low = new Color(0f, 0.5f, 1f);
    Color color_mid = new Color(0.94f, 0.39f, 0.67f);
    Color color_high = Color.yellow;
    public void ChangeGlowColor(float score, float magnitude){
        float mappedScore = (score + 1) / 2;
        // glow.startColor = Color.Lerp(color_low, color_high, mappedScore);
        // Debug.Log("change color");

        if(score<0){
            mappedScore = score+1;
            glow.startColor = Color.Lerp(color_low, color_mid, mappedScore);
        }
        else{
            glow.startColor = Color.Lerp(color_mid, color_high, mappedScore);
        }
    }
    public void StopGlowColor(){
        CancelInvoke("RandomGlowColor");
        // Debug.Log("stop random color");
    }
    public void ResetGlow(){
        glow.Stop();
        glow.startColor = Color.white;

        glowEnd = true;
        target.SetActive(false);
    }


    public void startHint(){
        // InvokeRepeating("Hint", 0.1f, 10f);
        hint.SetActive(true);
        track.SetActive(true);
        target.SetActive(true);
        Debug.Log("start hint");
    }
    public void startHintDelay(float second){
        Invoke("startHint", second);
    }
    void stopHint(){
        // CancelInvoke("Hint");
        hint.SetActive(false);
        track.SetActive(false);
        Debug.Log("stop hint");
    }
}
