using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialFinishControl : MonoBehaviour
{
    private GameObject[] context;
    private ParticleSystem effect;
    private int state = 2;

    void Start()
    {
        context = new GameObject[transform.childCount-1];
        for (int i = 0; i < transform.childCount-1; i++)
        {
            context[i] = transform.GetChild(i).gameObject;
        }
        effect = transform.GetChild(transform.childCount-1).gameObject.GetComponent<ParticleSystem>();
        ShowMethod(-1);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        //     ChangeState(true);
        // if (Input.GetKeyDown(KeyCode.W))
        //     ChangeState(false);
    }

    public void StartMethod(){
        effect.Play();
        ShowMethod(state);
    }

    public void ChangeState(bool isGood)
    {
        effect.Play();

        if(isGood)state--;
        else state++;

        ShowMethod(state);
    }

    private void ShowMethod(int methodIndex)
    {
        for (int i = 0; i < context.Length; i++)
        {
            if (i == methodIndex)
                context[i].SetActive(true);
            else
                context[i].SetActive(false);
        }
    }
}
