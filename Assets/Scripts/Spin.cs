using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spin : MonoBehaviour
{
    [SerializeField] float spinSpeedX = 0;
    [SerializeField] float spinSpeedY = 2;
    [SerializeField] float spinSpeedZ = 0;
    [SerializeField] GameObject thisGameObject;
    Transform thisTransform;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    void Update()
    {
        thisTransform.Rotate(spinSpeedX * 1f * Time.deltaTime, spinSpeedY * 1f * Time.deltaTime, spinSpeedZ * 1f * Time.deltaTime);
    }
}
