using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialControl : MonoBehaviour
{
    private GameObject[] context;
    private int page = -1;
    private GameObject change_c;

    void Awake()
    {
        context = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            context[i] = transform.GetChild(i).gameObject;
        }

        ShowPage(page);

        change_c = GameObject.Find("changePage");
    }

    public void ChangePage()
    {
        if (page > context.Length)return;
        else {
            page++;
            ShowPage(page);
        }

        if (page == context.Length-1){
            Debug.Log("projection finish");
            change_c.SetActive(false);
            GameManager.instance.startToTrial();
            page++;
        }
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

    public void CanChangePage(){
        change_c.SetActive(true);
    }
    public void EndPage(){
        ShowPage(-1);
    }

    public void PlaySlide(){     // 如果要自動播可以加
        ShowPage(0);
    }
}
