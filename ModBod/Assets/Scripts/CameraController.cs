using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private float lookOffsetMagnitude;
    [SerializeField] private Transform target;

    private float zPos;
	// Use this for initialization
	void Start () {
        zPos = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(target.position.x,target.position.y,zPos) + target.up * lookOffsetMagnitude;
	}
}
