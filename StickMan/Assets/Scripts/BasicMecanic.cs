using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMecanic : MonoBehaviour
{

    public GameObject ancors;
    public float gravityAir;
    public float gravityRope;
    public float factorX;
    public float factorY;

    private HingeJoint2D HJoint;
    private bool sticked = false;
    private Rigidbody2D rigidBody;
    private LineRenderer lineRenderer;
    private Vector2 positionActualJoint;
    private int lastBestPositionJoint;
    private int lastBestPositionSelected;

    void Start()
    {
        HJoint = gameObject.GetComponent<HingeJoint2D>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lastBestPositionJoint = 0;
        lastBestPositionSelected = 0;
        ancors.transform.GetChild(lastBestPositionSelected).gameObject.GetComponent<JointBehaviour>().selected();
    }

 
    void Update()
    {
        int bestPosition = 0;
        float bestDistance = float.MaxValue;
        for(int i = 0; i < ancors.transform.childCount; ++i)
        {
            float actualDistance = Vector2.Distance(gameObject.transform.position, ancors.transform.GetChild(i).transform.position);
            if(actualDistance < bestDistance)
            {
                bestPosition = i;
                bestDistance = actualDistance;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!sticked)
            {
                lineRenderer.enabled = true;
                HJoint.enabled = true;
                rigidBody.gravityScale = gravityRope;
                HJoint.connectedBody = ancors.transform.GetChild(bestPosition).transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
                positionActualJoint = ancors.transform.GetChild(bestPosition).gameObject.transform.position;
                ancors.transform.GetChild(bestPosition).gameObject.GetComponent<JointBehaviour>().setSticked();
                lastBestPositionJoint = bestPosition;
                lastBestPositionSelected = bestPosition;
                ancors.transform.GetChild(bestPosition).gameObject.GetComponent<JointBehaviour>().unselected();
            }
            else
            {
                ancors.transform.GetChild(lastBestPositionJoint).gameObject.GetComponent<JointBehaviour>().setUnsticked();
                rigidBody.velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x * factorX , gameObject.GetComponent<Rigidbody2D>().velocity.y + factorY) ;
                rigidBody.gravityScale = gravityAir;
                HJoint.enabled = false;
                lineRenderer.enabled = false;
                if (lastBestPositionSelected == bestPosition)
                {
                    ancors.transform.GetChild(bestPosition).gameObject.GetComponent<JointBehaviour>().selected();
                }
            }
            sticked = !sticked;
        }
        if (sticked)
        {
            lineRenderer.SetPosition(0, gameObject.transform.position); 
            lineRenderer.SetPosition(1, positionActualJoint);
        }
       
        if (lastBestPositionSelected != bestPosition)
        {
            ancors.transform.GetChild(lastBestPositionSelected).gameObject.GetComponent<JointBehaviour>().unselected();
            ancors.transform.GetChild(bestPosition).gameObject.GetComponent<JointBehaviour>().selected();
        }
        lastBestPositionSelected = bestPosition;
    }
}
