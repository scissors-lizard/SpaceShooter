using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour {
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject[] buildPiecePrefabs;
    private BuildPiece curPiece;

	// Use this for initialization
	void OnEnable () {
        player.Lock();

    }

    private void Update()
    {
        if(curPiece == null || !curPiece.enabled)
        {
            GameObject o = Instantiate(buildPiecePrefabs[0]) as GameObject;
            curPiece = o.GetComponent<BuildPiece>();
        }
    }

    // Update is called once per frame
    void OnDisable () {
        player.Unlock();
        Destroy(curPiece.gameObject);
    }
}
