using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float displacementAmount;
    MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        displacementAmount = Mathf.Lerp(displacementAmount, 0, Time.deltaTime);
        meshRenderer.material.SetFloat("_Displacement", displacementAmount);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            displacementAmount += 1.0f;
        }
    }
    public void addVolume(float v){
        displacementAmount+=v;
        // Debug.Log(displacementAmount);
    }
}
