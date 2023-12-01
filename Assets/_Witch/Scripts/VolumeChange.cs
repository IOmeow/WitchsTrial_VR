using UnityEngine;

public class VolumeChange : MonoBehaviour {

    AudioRecorder AudioRecorder;
    GameObject ball;
    void Start(){
        transform.localScale = Vector3.zero;
        AudioRecorder = GameObject.Find("SpeechControl").GetComponent<AudioRecorder>();
        
        ball = GameObject.Find("volumeBall");
        ball.SetActive(false);
    }

    public void startVolume(){
        InvokeRepeating("changeVolume", 3f, 0.1f);
        Debug.Log("Start Countdown 3s");
        // 加倒數3秒的動畫
    }
    public void stopVolume(){
        CancelInvoke("changeVolume");
        transform.localScale = Vector3.zero;
        ball.SetActive(false);
        Debug.Log("Stop Volume");
        // 加動畫中止
    }
    public void endVolume(){
        CancelInvoke("changeVolume");
        transform.localScale = Vector3.zero;
        ball.SetActive(false);
        Debug.Log("End Volume");
        // 加結束施法的動畫
    }

    float volume;
    void changeVolume(){
        ball.SetActive(true);
        volume = AudioRecorder.MicLoudness;
        if(volume>0){
            // transform.localScale = new Vector3(volume*1000f,volume*1000f,volume*1000f);
            ball.GetComponent<BallControl>().addVolume(volume*80);
        }
    }
}