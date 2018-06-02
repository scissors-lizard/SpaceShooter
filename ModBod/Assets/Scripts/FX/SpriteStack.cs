using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteStack : MonoBehaviour {
    [SerializeField] private int spriteCount;
    [SerializeField] private Vector3 globalOffset;

    private Transform[] sprites;
    private SpriteRenderer myRend;

	// Use this for initialization
	void Start () {
        sprites = new Transform[spriteCount];
        myRend = GetComponent<SpriteRenderer>();

        for (int i = 0; i < spriteCount; i++)
        {
            GameObject o = new GameObject(gameObject.name + "_s"+i);
            o.transform.SetParent(transform);
            SpriteHeightRenderer h = o.AddComponent<SpriteHeightRenderer>();
            h.baseTrans = transform;
            h.stackOrder = i;
            SpriteRenderer r = o.GetComponent<SpriteRenderer>();
            r.sprite = myRend.sprite;
            sprites[i] = o.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < spriteCount; i++)
        {
            sprites[i].transform.rotation = transform.rotation;
            sprites[i].transform.position = transform.position + globalOffset * i;

        }
	}
}
