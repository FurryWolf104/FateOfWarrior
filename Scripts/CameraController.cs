using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed= 0.5f;
    [SerializeField] GameObject target;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset = new Vector3(target.transform.position.x,target.transform.position.y,target.transform.position.z-10);
        transform.position = Vector3.Lerp(transform.position,offset, speed);
    }
}
