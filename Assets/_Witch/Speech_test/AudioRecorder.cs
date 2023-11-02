using UnityEngine;
using System.Collections;
using System;

public class AudioRecorder : MonoBehaviour
{
    private AudioSource audioSource;
    string device;

    bool micro = false;
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