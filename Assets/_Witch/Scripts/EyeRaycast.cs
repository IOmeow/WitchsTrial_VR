using UnityEngine;

public class EyeRaycast : MonoBehaviour
{
    public float maxRaycastDistance = 10f;
    public LayerMask methodLayer;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRaycastDistance, methodLayer))
        {
            if(hit.collider.gameObject.name=="Slide")GameManager.instance.lookAtSlide = true;
            else GameManager.instance.lookAtSlide = false;

            // if(hit.collider.gameObject.name=="black_board")GameManager.instance.lookAtBlackBoard = true;
            // else GameManager.instance.lookAtBlackBoard = false;
        }
        else {
            GameManager.instance.lookAtSlide = false;
            // GameManager.instance.lookAtBlackBoard = false;
        }
    }
}
