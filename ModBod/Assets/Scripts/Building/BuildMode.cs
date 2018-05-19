using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour {
    [SerializeField] private PlayerController player;
    [SerializeField] private Body body;
    [SerializeField] private GameObject[] buildPiecePrefabs;
    private Buildable curBuildable;

	// Use this for initialization
	void OnEnable () {
        player.Lock();

    }

    private void Update()
    {
        if(curBuildable == null || !curBuildable.enabled)
        {
            GameObject o = Instantiate(buildPiecePrefabs[0]) as GameObject;
            curBuildable = o.GetComponent<Buildable>();
        }

        if (Input.GetMouseButtonDown(1))
        {
            curBuildable.Attach();
        }
    }

    // Update is called once per frame
    void OnDisable () {
        player.Unlock();
        body.SetBuildMode(false);
        Destroy(curBuildable.gameObject);
    }
}
