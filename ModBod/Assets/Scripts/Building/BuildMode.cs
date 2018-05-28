using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour {
    [SerializeField] private PlayerController player;
    [SerializeField] private Body body;
    [SerializeField] private GameObject[] buildPiecePrefabs;
    [SerializeField] private GameObject buildCursor;

    private Buildable curBuildable;
    private Vector2 lastBuildableRotation; 

	// Use this for initialization
	void OnEnable () {
        player.Lock();
        body.SetBuildMode(true);
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
            lastBuildableRotation = curBuildable.transform.up;
            curBuildable.Attach();
        }
        BuildCell cell = body.GetCellAtPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(cell != null)
        {
            buildCursor.transform.position = body.transform.TransformPoint(cell.localPos);
        }

    }

    // Update is called once per frame
    void OnDisable () {
        player.Unlock();
        body.SetBuildMode(false);
        Destroy(curBuildable.gameObject);
    }
}
