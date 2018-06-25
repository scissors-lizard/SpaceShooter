using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteCycle : MonoBehaviour {
    public float frameDuration;
    public Sprite[] frames;

    private SpriteRenderer rend;
    private float timer = 0f;
    private int index = 0;

	// Use this for initialization
	void Start () {
        timer = frameDuration;
        rend = GetComponent<SpriteRenderer>();
        index = (int)(Time.timeSinceLevelLoad / frameDuration) % frames.Length;
        rend.sprite = frames[index];
        timer = Time.timeSinceLevelLoad % frameDuration;

    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer += frameDuration;
            index++;
            if(index > frames.Length-1)
            {
                index -= frames.Length;
            }
            rend.sprite = frames[index];
        }
	}
}
