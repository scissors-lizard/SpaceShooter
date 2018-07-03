using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour {
    public SpriteRenderer[] renderers;

    [SerializeField] private Color flashColor;
    [SerializeField] private float flashDur, gapDur;
    [SerializeField] private int flashes;
    [SerializeField] private SpriteStack spriteStack;

    private Coroutine flow;
    private Color startColor;

    public void Start()
    {
        if (spriteStack != null)
        {
            renderers = spriteStack.renderers;
        }
        else
        {
            renderers = new SpriteRenderer[1];
            renderers[0] = GetComponent<SpriteRenderer>();
        }
    }

    public void Flash()
    {
        if(flow != null)
        {
            StopCoroutine(flow);
            SetColor(startColor);
        }
        flow = StartCoroutine(FlashFlow());
    }

    private IEnumerator FlashFlow()
    {
        startColor = renderers[0].color;
        float timer;
        for (int i = 0; i < flashes; i++)
        {
            SetColor(flashColor);
            timer = flashDur;
            while (timer > 0f)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            SetColor(startColor);
            timer = gapDur;
            while (timer > 0f)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

        }

    }

    private void SetColor(Color c)
    {
        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = c;
        }
    }
	
}
