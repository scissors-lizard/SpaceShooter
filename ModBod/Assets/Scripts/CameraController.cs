using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private float lookOffsetMagnitude;
    [SerializeField] private Transform target;
    [SerializeField] private Camera cam;

    private float zPos;

    private Vector3 targetPos;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
        zPos = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        targetPos = new Vector3(target.position.x,target.position.y,zPos) + (mousePos - target.position) * lookOffsetMagnitude;
        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);




        Vector3 point = cam.WorldToViewportPoint(targetPos);
        Vector3 delta = targetPos - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
