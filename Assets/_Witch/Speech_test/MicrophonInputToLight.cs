using UnityEngine;

public class MicrophoneInputToLight : MonoBehaviour
{
    public Color color_low; // 顏色1
    public Color color_high; // 顏色2
    private ParticleSystem glow;

    void Start()
    {
        glow = GameObject.Find("glow").GetComponent<ParticleSystem>();
    }

    void Update()
    {

    }

    public void ChangeColor(float score, float magnitude){
        float mappedScore = (score + 1) / 2;
        Debug.Log("change color");
        // 將顏色設置為兩種顏色之間的插值
        // render.material.color = Color.Lerp(color_low, color_high, mappedScore);
        // sphere.transform.localScale = new Vector3(3f+2f*magnitude, 3f+2f*magnitude, 3f+2f*magnitude);
        
    }
}