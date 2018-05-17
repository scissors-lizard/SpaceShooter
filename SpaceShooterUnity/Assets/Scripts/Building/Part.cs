using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Part : MonoBehaviour {
	[SerializeField] protected int maxHp;

	public virtual void OnKilled (){

	}
}
