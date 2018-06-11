using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteStack : MonoBehaviour {
    [SerializeField] private int spriteCount;
    [SerializeField] private Vector3 globalOffset;

    public SpriteRenderer[] renderers;

	// Use this for initialization
	void Awake () {
        renderers = new SpriteRenderer[spriteCount];
        renderers[0] = GetComponent<SpriteRenderer>();

        for (int i = 1; i < spriteCount; i++)
        {
            GameObject o = new GameObject(gameObject.name + "_s"+i);
            o.transform.SetParent(transform);
            o.transform.localScale = new Vector3(1f, 1f, 1f);
            SpriteRenderer r = o.AddComponent<SpriteRenderer>();
            r.sprite = renderers[0].sprite;
            renderers[i] = r;
        }

    }
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < spriteCount; i++)
        {
            renderers[i].transform.rotation = transform.rotation;
            renderers[i].transform.position = transform.position + globalOffset * i;
        }
        SetRenderOrder((int)(-transform.position.y * 1000));
	}

    public void SetRenderOrder(int value)
    {
        for(int i=0;i<spriteCount;i++)
        {
            renderers[i].sortingOrder = value + i;
        }
    }
}
