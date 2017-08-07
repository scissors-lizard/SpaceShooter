using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashRedonHit : MonoBehaviour {
	[SerializeField] private SpriteRenderer sprite;
	private Coroutine flashRoutine;
	private Color startColor;

	void Start(){
		startColor = sprite.color;
	}

	void OnParticleCollision(GameObject pSys){
		if (flashRoutine == null) {
			flashRoutine = StartCoroutine (FlashRed ());
		}
		Debug.Log ("Hit2D");
	}

	private IEnumerator FlashRed(){
		for (int i = 0; i < 1; i++) {
			sprite.color = Color.red;
			yield return new WaitForSeconds(0.1f);
			sprite.color = startColor;
			yield return new WaitForSeconds(0.1f);

		}
		flashRoutine = null;
	}
}
