using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShepherdConcept : MonoBehaviour
{
    public WaitForSeconds waitTimer = new WaitForSeconds(10);
    public int frequency = 6;
    public float x;
    public float z;

    public GameObject terrain;
    public float speed = .5f;

    void Start() {
        
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, GetPos()) < 0.1f)
        {
            SetPos();
        }
        else Go();
    }

    void SetPos() 
    {
        var parent = terrain.GetComponent<MeshFilter>().mesh.bounds;
        x = Random.Range(parent.min.x, parent.max.x);
        z = Random.Range(parent.min.z, parent.max.z);
    }

    public Vector3 GetPos() 
    {
        Vector3 pos = new Vector3();
        pos.x = x;
        pos.z = z;
        return pos;
    }

    void Go() 
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GetPos(), step);
    }
}