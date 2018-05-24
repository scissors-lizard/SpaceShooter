using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float torque, rotDecelAngle;
    [SerializeField] private Body body;

    private void Start()
    {
        rb.mass = body.mass;
    }

    // Update is called once per frame
    void FixedUpdate () {
        rb.AddForce( speed * (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) );

        UpdateRotation();
	}

    private void UpdateRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var targetDir = (mousePos - transform.position).normalized;
        var forward = transform.up;

        float angle = Vector2.SignedAngle(forward, targetDir);
        if (Mathf.Abs(angle) > 0.01f)
        {
            float rotVel = Mathf.Clamp(angle, -5f, 5f) * torque / 5f;

            if (Mathf.Abs(angle) < rotDecelAngle) // inside decel zone
            {
                rotVel /= Mathf.Abs(rotVel);
                rotVel *= torque * Mathf.Abs(angle) / rotDecelAngle;
            }

            rb.angularVelocity = rotVel / rb.mass;
        }
        rb.centerOfMass = Vector2.zero;
    }

    public void Lock()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        enabled = false;
        body.SetBuildMode(true);
    }

    public void Unlock()
    {
        rb.isKinematic = false;
        enabled = true;
        body.SetBuildMode(false);
    }
}
