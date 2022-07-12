using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField] Transform _follorTarget;

    // Update is called once per frame
    void Update()
    {
        transform.position = _follorTarget.position;
    }
}
