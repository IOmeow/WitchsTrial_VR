using UnityEngine;

public class final_hint : MonoBehaviour
{
    private Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(GameManager.instance.tutorial)return;
        // if (GameManager.instance.lookAtBlackBoard) myAnimator.SetBool("hint", true);
        // else myAnimator.SetBool("hint", false);
    }
}
