using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotFX : MonoBehaviour {
    [SerializeField] private float duration;

    private float timer;
	// Use this for initialization
	void Start () {
        timer = duration;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            Destroy(gameObject);
        }

	}
}
