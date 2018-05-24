using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {
    [SerializeField] private BuildMode buildMode;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buildMode.gameObject.SetActive(!buildMode.gameObject.activeInHierarchy);

        }
	}
}
