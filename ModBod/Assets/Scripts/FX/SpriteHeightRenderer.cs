using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteHeightRenderer : MonoBehaviour {
    public Transform baseTrans;
    public int stackOrder = 0;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start ()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sprite.sortingOrder = Mathf.RoundToInt(baseTrans.position.y * 1000f) * -1 + stackOrder;
    }
}
