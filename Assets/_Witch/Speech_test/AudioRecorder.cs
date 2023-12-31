using UnityEngine;
using System.Collections;
using System;

public class AudioRecorder : MonoBehaviour
{
    private AudioSource audioSource;
    string device;

    bool micro = false;
    public static float MicLoudness;

    void Start(){
        audioSource = GetComponent<AudioSource>();

        if (Microphone.devices.Length > 0)
        {
            device = Microphone.devices[0];
            Debug.Log("使用的麥克風：" + device);
            audioSource.clip = Microphone.Start(device, true, 10, 16000);
            micro = true;
        }
        else
        {
            Debug.Log("找不到麥克風");
        }
    }
    void OnDisable(){
        Microphone.End(device);
    }
    void OnDestroy(){
        Microphone.End(device);
    }

    int _sampleWindow = 128;
    //get data from microphone into audioclip
    float  LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null)-(_sampleWindow+1); // null means the first microphone
        if (micPosition < 0) return 0;
        audioSource.clip.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++) {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }
    void Update(){
        MicLoudness = LevelMax ();
    }

    public bool CheckMicrophone()
    {
        return micro;
    }

    public void StartRecording()
    {
        StartCoroutine("start_recording");
    }
    IEnumerator start_recording()
    {
        // audioSource.clip = Microphone.Start(device, true, 10, 16000);
        Debug.Log("StartRecording");
        // audioSource.Play();
        yield return null;
    }

    public void StopRecording()
    {
        StartCoroutine("stop_recording");
    }
    IEnumerator stop_recording()
    {
        // Microphone.End(device);
        Debug.Log("StopRecording");
        // audioSource.Pause();
        SavWav.Save("recordedAudio", audioSource.clip);
        yield return null;
    }
}