using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialControl : MonoBehaviour
{
    private GameObject[] context;
    private int page = -1;
    private GameObject toChange;

    void Awake()
    {
        context = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            context[i] = transform.GetChild(i).gameObject;
        }

        ShowPage(page);

        toChange = GameObject.Find("changePage");
        toChange.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ChangePage();
    }

    public void ChangePage()
    {
        toChange.SetActive(true);
        page++;

        if (page >= context.Length){
            Debug.Log("projection finish");
            GameManager.instance.startToMagic();
            toChange.SetActive(false);
        }
        else ShowPage(page);
    }

    private void ShowPage(int pageIndex)
    {
        for (int i = 0; i < context.Length; i++)
        {
            if (i == pageIndex)
                context[i].SetActive(true);
            else
                context[i].SetActive(false);
        }
    }
}
