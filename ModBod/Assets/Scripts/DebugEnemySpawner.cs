using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;
            Instantiate(enemyPrefab, worldPos, Quaternion.identity);
        }
	}
}
