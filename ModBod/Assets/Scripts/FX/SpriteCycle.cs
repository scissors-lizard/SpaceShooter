using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCycle : MonoBehaviour {
    [SerializeField] private float frameDuration;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private Sprite[] frames;

    private float timer = 0f;
    private int index = 0;

	// Use this for initialization
	void Start () {
        timer = frameDuration;
        rend.sprite = frames[0];

    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer += frameDuration;
            index++;
            if(index > frames.Length)
            {
                index -= frames.Length;
            }
            rend.sprite = frames[index];
        }
	}
}
