using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Rotate360 : MonoBehaviour
{

    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.position.x * Time.deltaTime * speed, transform.position.y * Time.deltaTime * speed, transform.position.z * Time.deltaTime * speed, Space.World);
    }
}
