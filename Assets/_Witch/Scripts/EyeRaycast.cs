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
            GameManager.instance.lookAtSlide = true;
            // Debug.Log(GameManager.instance.lookAtSlide);
        }
        else {
            GameManager.instance.lookAtSlide = false;
        }
    }
}
