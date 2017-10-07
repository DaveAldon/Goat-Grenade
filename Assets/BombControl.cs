using UnityEngine;

public class BombControl : MonoBehaviour
{
    Ray ray;
    RaycastHit rayHit;
    Vector3 point;
    private const int DropHeight = 2;
    public GameObject obj;

    void Update()
    {                                                     
        if (Input.GetMouseButtonDown(0))
        {
            //TODO: ray assignment can be moved outside of click event if we want target highlighting control
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out rayHit))
            {
                point = rayHit.point;
                point.y = DropHeight;
                Instantiate(obj, point, Quaternion.identity);
            }
        }   
    }
}