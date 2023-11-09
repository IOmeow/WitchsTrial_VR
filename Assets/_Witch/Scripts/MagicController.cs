using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    Control speech;
    // MagicRotate magicHint;

    private GameObject trail;
    private ParticleSystem glow;

    SoundControl sound;
    // Start is called before the first frame update
    void Start()
    {
        speech = GameObject.Find("=== System ===").GetComponent<Control>();
        sound = GameObject.Find("=== System ===").GetComponent<SoundControl>();
        // magicHint = GameObject.Find("Magic_hint").GetComponent<MagicRotate>();

        glow = GameObject.Find("glow").GetComponent<ParticleSystem>();
        trail = GameObject.Find("trail");
        trail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))startHint();
        if(Input.GetKeyDown(KeyCode.N))stopHint();
    }
    
    int t = 0;
    bool MagicStart = false, glowEnd = true;
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "track1" && t==0 && glowEnd){
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

            glow.Play();
            sound.playMagigSE();

            stopHint();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name == "StayTarget" && MagicStart){
            MagicStart=false;
            glowEnd = false;
            speech.stopRecord();

            InvokeRepeating("RandomGlowColor", 0.1f, 1f);
            trail.SetActive(false);

            // sound.playMagigSE();
        }
    }
    void ResetTrack(){
        t=0;
        // Debug.Log("Reset track");
        trail.SetActive(false);;
    }

    void RandomGlowColor(){
        glow.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    Color color_low = Color.cyan;
    Color color_high = Color.yellow;
    public void ChangeGlowColor(float score, float magnitude){
        float mappedScore = (score + 1) / 2;
        glow.startColor = Color.Lerp(color_low, color_high, mappedScore);
        // Debug.Log("change color");
    }
    public void StopGlowColor(){
        CancelInvoke("RandomGlowColor");
        // Debug.Log("stop random color");
    }
    public void ResetGlow(){
        glow.Stop();
        glow.startColor = Color.white;

        glowEnd = true;
    }


    public void startHint(){
        InvokeRepeating("Hint", 0.1f, 10f);
    }
    void Hint(){
        // magicHint.toStart();
    }
    void stopHint(){
        CancelInvoke("Hint");
    }
}
