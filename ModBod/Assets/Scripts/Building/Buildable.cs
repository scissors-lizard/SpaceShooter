using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BodyPart))]
public class Buildable : MonoBehaviour {

    public bool Snapping { get { return snapping; } }
    public BodyPart part;

    private Camera cam;
    private bool snapping = false;
    private GameObject colliderHolder;
    private Vector2 lastSnapPos;
    private Vector3 targetPos, targetRot;
    private Body body;
    private BuildCell targetCell;
    private Vector3 mouseWorldPos;
    private Vector2 desiredUp;
    private Dir targetDir;
    private bool targetingValidPlacement = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    // Use this for initialization
    void Start () {
        part = GetComponent<BodyPart>();
        part.SetBuildMode(true);
        body = BuildMode.Instance.body;
        desiredUp = Vector2.up;
    }


    // Update is called once per frame
    void Update () {
        CheckMousePos();
        desiredUp = Quaternion.Euler(0f,0f,-Input.GetAxis("Scroll") * Time.deltaTime * 7000f) * desiredUp;
        if (!snapping)
        {
            targetPos = mouseWorldPos;
            targetRot = desiredUp;
        }
        else // snapping
        {
            targetPos = body.transform.TransformPoint(targetCell.localPos);
            Vector2 localVec = body.transform.InverseTransformDirection(desiredUp);
            targetDir = VectorToNearestDir(localVec);
            targetRot = body.transform.TransformDirection(targetDir.ToVector2());

            CheckValidPlacement();

            if (targetingValidPlacement && Input.GetMouseButtonDown(1))
            {
                Attach();
            }
        }

        UpdateTransform();
	}

    public static Dir VectorToNearestDir(Vector2 direction)
    {
        Dir retVal = Dir.N; // default
        float highest = -2f;
        Vector2 normDir = direction.normalized;

        float dp = Vector2.Dot(normDir, Dir.N.ToVector2());
        if (dp > highest)
        {
            highest = dp;
            retVal = Dir.N;
        }

        dp = Vector2.Dot(normDir, Dir.E.ToVector2());
        if (dp > highest)
        {
            highest = dp;
            retVal = Dir.E;
        }

        dp = Vector2.Dot(normDir, Dir.S.ToVector2());
        if (dp > highest)
        {
            highest = dp;
            retVal = Dir.S;
        }

        dp = Vector2.Dot(normDir, Dir.W.ToVector2());
        if (dp > highest)
        {
            highest = dp;
            retVal = Dir.W;
        }

        return retVal;
    }

    void UpdateTransform()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 8f);
        transform.up = Vector3.Lerp(transform.up, targetRot, Time.deltaTime * 16f);
    }

    private void CheckMousePos()
    {
        mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        if (body.IsMouseOverValidSlot(mouseWorldPos)) { 
            targetCell = body.GetCellAtPos(mouseWorldPos);
            snapping = true;
        }
        else
        {
            snapping = false;
            targetingValidPlacement = false;
        }
    }

    void CheckValidPlacement()
    {
        targetingValidPlacement = body.CheckValidPlacement(targetCell, part, targetDir);

    }

    public void Attach()
    {
        if (snapping)
        {

            transform.position = targetPos;
            transform.up = targetRot;
            part.body = body;
            part.gridCell = targetCell;
            body.AddPart(part, targetCell.col, targetCell.row, targetDir);
            body.UpdateHighlights();

            Destroy(this);
        }
    }
}
