using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    Control speech;
    public GameObject TrackHint;
    private ParticleSystem p_hint;
    private ParticleSystem p_recording;

    private GameObject trail;
    private ParticleSystem glow;

    SoundControl sound;
    // Start is called before the first frame update
    void Start()
    {
        speech = GameObject.Find("=== System ===").GetComponent<Control>();
        sound = GameObject.Find("=== System ===").GetComponent<SoundControl>();

        p_hint = TrackHint.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        p_recording = TrackHint.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

        p_hint.Play();

        trail = GameObject.Find("trail");
        trail.SetActive(false);
        glow = GameObject.Find("glow").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)){
            pRecordFinish();
            p_hint.Play();
        }
        if(Input.GetKeyDown(KeyCode.J)){
            p_hint.Stop();
            pRecording();
        }

    }
    
    int t = 0;
    bool MagicStart = false;
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "track1" && t==0){
            t=1;
            Invoke("ResetTrack", 5);
            Debug.Log("track1");
            trail.SetActive(true);
        }
        if(other.name == "track2" && t==1){
            t=2;
            Debug.Log("track2");
        }
        if(other.name == "track3" && t==2){
            t=3;
            Debug.Log("track3");
        }
        if(other.name == "track4" && t==3){
            t=4;
            Debug.Log("track4");
        }
        if(other.name == "StayTarget" && t==4 && !MagicStart){
            MagicStart = true;
            speech.startRecord();

            p_hint.Stop();
            pRecording();

            glow.Play();
            sound.playMagigSE();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name == "StayTarget" && MagicStart){
            MagicStart=false;
            speech.stopRecord();

            pRecordFinish();
            p_hint.Play();

            InvokeRepeating("RandomGlowColor", 0.1f, 1f);
            trail.SetActive(false);

            sound.playMagigSE();
        }
    }
    void ResetTrack(){
        t=0;
        // Debug.Log("Reset track");
        if(!MagicStart)trail.SetActive(false);;
    }

    void pRecording(){
        Color nowColor;
        ColorUtility.TryParseHtmlString("#AFFFB257", out nowColor);
        p_recording.startColor = nowColor;
        p_recording.Play();
    }
    void pRecordFinish(){
        p_recording.Stop();
        Color nowColor;
        ColorUtility.TryParseHtmlString("#FFB7AF57", out nowColor);
        p_recording.startColor = nowColor;
    }

    void RandomGlowColor(){
        glow.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    Color color_low = Color.cyan;
    Color color_high = Color.yellow;
    public void ChangeGlowColor(float score, float magnitude){
        float mappedScore = (score + 1) / 2;
        glow.startColor = Color.Lerp(color_low, color_high, mappedScore);
        Debug.Log("change color");
    }
    public void StopGlowColor(){
        CancelInvoke("RandomGlowColor");
        Debug.Log("stop random color");
    }
    public void ResetGlow(){
        glow.Stop();
        glow.startColor = Color.white;
    }
}
