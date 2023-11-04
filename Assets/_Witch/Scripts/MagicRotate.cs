using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRotate : MonoBehaviour
{
    public float _distance = 0.2f;
    public float _speed = 20f;
    public float _angle = 0.7f;

    GameObject trail;

    bool _isStart = false;

    Transform _center;
    Transform _surround;
    float _value = 1;

    void Start(){
        trail = GameObject.Find("hint_trail");
    }

    public void toStart()
    {
        trail.SetActive(false);
        _value = 1;
        _center = transform;
        _surround = transform.GetChild(0).gameObject.transform;
        _center.name = "_center";
        _surround.name = "_surround";

        _surround.position = Vector3.Normalize(Vector3.down)*_distance+_center.position;
        _isStart = true;
        trail.SetActive(true);
    }

    void Update()
    {
        if(!_isStart)return;
        else updatePos();
    }

    void updatePos(){
        _value -= _speed*Time.deltaTime*0.01f;
        float distance = Mathf.Lerp(0, _distance, _value);

        Quaternion rotate = Quaternion.AngleAxis(_angle, Vector3.back);
        Vector3 dir = Vector3.Normalize(_surround.position - _center.position);
        _surround.position = rotate*dir*distance+_center.position;
    }
}
