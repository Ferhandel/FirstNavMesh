using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float damp = 2;
    public float displacement = 3f;
    float pingpong;
    float height;
    // Start is called before the first frame update
    void Start()
    {
        height = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        pingpong = Mathf.SmoothStep(-1, 1, Mathf.PingPong(Time.time / damp, 1));
        transform.localPosition = Vector3.up * height + transform.forward * pingpong * displacement;
    }
}
