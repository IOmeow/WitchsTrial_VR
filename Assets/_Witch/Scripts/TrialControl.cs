using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialControl : MonoBehaviour
{
    private GameObject[] context;
    private int page = -1;
    private GameObject change_c, page_hint;

    void Awake()
    {
        context = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            context[i] = transform.GetChild(i).gameObject;
        }

        ShowPage(page);

        change_c = GameObject.Find("changePage");
        page_hint = GameObject.Find("TurnPageHint");
        change_c.GetComponent<BoxCollider>().enabled = false;
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
            change_c.GetComponent<BoxCollider>().enabled = false;
            page_hint.SetActive(false);
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
        StartCoroutine(WaitForlLookAtSlide());
    }
    IEnumerator WaitForlLookAtSlide()
    {
        yield return new WaitUntil(() => GameManager.instance.lookAtSlide == true);
        
        change_c.GetComponent<BoxCollider>().enabled = true;
        page_hint.SetActive(true);
        page_hint.GetComponent<MeshRenderer>().enabled = true;
    }

    public void EndPage(){
        ShowPage(-1);
    }

    public void PlayEndSlide(int end){
        StartCoroutine(ShowPagesWithDelay(end));
    }

    IEnumerator ShowPagesWithDelay(int end)
    {
        ShowPage(0);
        // 加音效
        yield return new WaitForSeconds(10f);
        if(end==0) ShowPage(1);
        else if(end==1){
            if(GameManager.instance.score1>0) ShowPage(2);
            else ShowPage(3);
        }
        else ShowPage(4);
        
        VoiceOverControl.instance.playTrial(end+5, true);
    }
}
