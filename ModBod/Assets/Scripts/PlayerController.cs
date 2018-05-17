using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private BodyPart[] bodyParts;
    [SerializeField] private float rotSpeed, rotDecelAngle;

    private void Start()
    {
        float sumMass = 0f;
        for (int i = 0; i < bodyParts.Length; i++)
        {
            sumMass += bodyParts[i].mass;
        }
        rb.mass = sumMass;
    }

    // Update is called once per frame
    void FixedUpdate () {
        rb.AddForce( speed * (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) );

        UpdateRotation();
	}

    private void UpdateRotation()
    {
        /*
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var targetDir = (mousePos - transform.position).normalized;
        var forward = transform.up;

        float angle = Vector2.SignedAngle(forward, targetDir);
        if (Mathf.Abs(angle) > 0.01f)
        {

            float rotVel = Mathf.Clamp(angle, -rotSpeed, rotSpeed);

            if (Mathf.Abs(angle) < rotDecelAngle) // inside decel zone
            {
                rotVel /= Mathf.Abs(rotVel);
                rotVel *= rotSpeed * Mathf.Abs(angle) / rotDecelAngle;
            }

            transform.Rotate(0f, 0f, rotVel);
        }*/

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var targetDir = (mousePos - transform.position).normalized;
        var forward = transform.up;

        float angle = Vector2.SignedAngle(forward, targetDir);
        if (Mathf.Abs(angle) > 0.01f)
        {

            float rotVel = Mathf.Clamp(angle, -5f, 5f) * rotSpeed / 5f;

            if (Mathf.Abs(angle) < rotDecelAngle) // inside decel zone
            {
                rotVel /= Mathf.Abs(rotVel);
                rotVel *= rotSpeed * Mathf.Abs(angle) / rotDecelAngle;
            }

            rb.angularVelocity = rotVel;
        }
    }


}
