using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    private Volume volume;
    private VolumeProfile volumeProfile;
    private ColorAdjustments colorAdjustments;

    private float hueShiftSpeed = 50f; // 調整 Hue Shift 的速度

    private bool good_changing=false, bad_changing=false;

    void Start()
    {
        volume = this.GetComponent<Volume>();
        volumeProfile = volume.profile;

        if (!volumeProfile.TryGet(out colorAdjustments))
        {
            colorAdjustments = volumeProfile.Add<ColorAdjustments>();
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.M))ChangeLight(true);
        if(Input.GetKeyDown(KeyCode.N))ChangeLight(false);
        if(Input.GetKeyDown(KeyCode.B))ResetLigt();

        if(good_changing){
            GoodChange();
        }
        if(bad_changing){
            BadChange();
        }
    }

    void GoodChange(){
        float newHueShiftValue = Mathf.PingPong(Time.time * hueShiftSpeed, 100f) + 30f;
        colorAdjustments.hueShift.value = newHueShiftValue;
    }
    void BadChange(){
        float newHueShiftValue = Mathf.PingPong(Time.time * hueShiftSpeed, 100f) -130f;
        colorAdjustments.hueShift.value = newHueShiftValue;
    }

    // float HueShiftValue = -30;
    // void changing(){
    //     newHueShiftValue = (newHueShiftValue+1)%360;
    //     colorAdjustments.hueShift.value = newHueShiftValue-180;
    // }

    public void ChangeLight(bool high){
        if(high){
            colorAdjustments.saturation.value=25;
            // InvokeRepeating("changing", 0f, 0.01f);
            good_changing = true;
        }
        else {
            colorAdjustments.saturation.value=-100;
            // InvokeRepeating("changing", 0f, 0.05f);
            bad_changing = true;
        }
    }

    public void ResetLigt(){
        // CancelInvoke("changing");
        good_changing = false;
        bad_changing = false;
        colorAdjustments.saturation.value = 25;
        colorAdjustments.hueShift.value = -30;
    }
}
