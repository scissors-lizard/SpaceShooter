using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthColorSetter : MonoBehaviour {
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private BodyPart part;
    [SerializeField] private SpriteRenderer sprite;

	// Update is called once per frame
	void Update () {
        sprite.color = colorGradient.Evaluate((float)part.curHP / part.maxHP);
    }

}
