using UnityEngine;

public class VolumeChange : MonoBehaviour {

    AudioRecorder AudioRecorder;
    GameObject ball, triangle;
    public Transform targetObject;
    void Start(){
        AudioRecorder = GameObject.Find("SpeechControl").GetComponent<AudioRecorder>();
        
        ball = transform.GetChild(0).gameObject;
        ball.SetActive(false);

        triangle = GameObject.Find("triangle");
        triangle.SetActive(false);
    }

    public void startVolume(){
        InvokeRepeating("changeVolume", 3f, 0.1f);
        Invoke("setVolume", 3f);
        Debug.Log("Start Countdown 3s");
        // 倒數3秒的動畫
        triangle.SetActive(true);
    }
    public void stopVolume(){
        CancelInvoke("changeVolume");
        CancelInvoke("setVolume");
        ball.SetActive(false);
        // Debug.Log("Stop Volume");
        // 加動畫中止
        triangle.SetActive(false);
    }
    public void endVolume(){
        CancelInvoke("changeVolume");
        ball.SetActive(false);
        // Debug.Log("End Volume");
        // 加結束施法的動畫
    }

    float volume;
    void changeVolume(){
        volume = AudioRecorder.MicLoudness;
        if(volume>0){
            ball.GetComponent<BallControl>().addVolume(volume*80);
        }
    }
    void setVolume(){
        triangle.SetActive(false);
        // 換位置
        this.transform.position = targetObject.position;
        ball.SetActive(true);
    }
}