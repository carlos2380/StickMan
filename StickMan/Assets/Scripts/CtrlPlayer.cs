using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlPlayer : MonoBehaviour
{

    public GameObject ancors;
    public float gravityAir;
    public float gravityRope;
    public float factorX;
    public float factorY;
    [Header("Sprites player")]
    public Sprite ballSprite;
    public Sprite stickedStopedSprite;
    public Sprite stickedGoSprite;
    public Sprite stickedBackSprite;
    public Sprite winSprite;

    private HingeJoint2D hJoint;
    private Rigidbody2D rigidBody;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;
    private Vector3 positionActualJoint;
    private bool sticked;
    private int lastBestPositionJoint;
    private int lastBestPositionSelected;
    private int bestPosition;
    private float bestDistance;
    private bool won;
    private int touches;
    void Start()
    {
        hJoint = gameObject.GetComponent<HingeJoint2D>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        sticked = false;
        lastBestPositionJoint = 0;
        lastBestPositionSelected = 0;
        ancors.transform.GetChild(lastBestPositionSelected).gameObject.GetComponent<JointBehaviour>().selected();
        won = false;
        touches = 0;
    }

    void Update()
    {
        bestPosition = 0;
        bestDistance = float.MaxValue;
        for(int i = 0; i < ancors.transform.childCount; ++i)
        {
            float actualDistance = Vector2.Distance(gameObject.transform.position, ancors.transform.GetChild(i).transform.position);
            if(actualDistance < bestDistance)
            {
                bestPosition = i;
                bestDistance = actualDistance;
            }
        }

        if(!won)
        {
            checkInput();
        }

        if (sticked)
        {
            lineRenderer.SetPosition(0, gameObject.transform.position); 
            lineRenderer.SetPosition(1, positionActualJoint);
            printPlayer();
        }
       
        if (lastBestPositionSelected != bestPosition)
        {
            ancors.transform.GetChild(lastBestPositionSelected).gameObject.GetComponent<JointBehaviour>().unselected();
            ancors.transform.GetChild(bestPosition).gameObject.GetComponent<JointBehaviour>().selected();
        }
        lastBestPositionSelected = bestPosition;
    }

    private void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || ((Input.touchCount > 0) && (touches == 0)))
        {
            
            lineRenderer.enabled = true;
            hJoint.enabled = true;

            rigidBody.gravityScale = gravityRope;

            hJoint.connectedBody = ancors.transform.GetChild(bestPosition).transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
            positionActualJoint = ancors.transform.GetChild(bestPosition).gameObject.transform.position;

            ancors.transform.GetChild(bestPosition).gameObject.GetComponent<JointBehaviour>().setSticked();
            ancors.transform.GetChild(bestPosition).gameObject.GetComponent<JointBehaviour>().unselected();                  ;

            lastBestPositionJoint = bestPosition;

            rigidBody.angularVelocity = 0f;
            sticked = !sticked;
        }
        if (Input.GetKeyUp(KeyCode.Space) || ((Input.touchCount == 0) && (touches > 0)))
        {
            hJoint.enabled = false;
            lineRenderer.enabled = false;

            rigidBody.velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x * factorX, gameObject.GetComponent<Rigidbody2D>().velocity.y + factorY);
            rigidBody.gravityScale = gravityAir;

            ancors.transform.GetChild(lastBestPositionJoint).gameObject.GetComponent<JointBehaviour>().setUnsticked();
            if (bestPosition == lastBestPositionJoint)
            {
                ancors.transform.GetChild(bestPosition).gameObject.GetComponent<JointBehaviour>().selected();
                ancors.transform.GetChild(lastBestPositionSelected).gameObject.GetComponent<JointBehaviour>().unselected();
            }

            spriteRenderer.sprite = ballSprite;
            rigidBody.AddTorque(-rigidBody.velocity.magnitude);
            sticked = !sticked;
        }
        touches = Input.touchCount;
    }

    private void printPlayer()
    {
        if (rigidBody.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }else
        {
            spriteRenderer.flipX = true;
        }

        if (rigidBody.velocity.x < 0.7f && rigidBody.velocity.x > -0.7f && gameObject.transform.position.y < positionActualJoint.y)
        {
            spriteRenderer.sprite = stickedStopedSprite;
        }
        else
        {
            if (rigidBody.velocity.y < 0)
            {
                spriteRenderer.sprite = stickedGoSprite;
            }
            else
            {
                spriteRenderer.sprite = stickedBackSprite;
            }
        }
        
        gameObject.transform.eulerAngles = LookAt2d(positionActualJoint);
    }

    public bool getSticked()
    {
        return sticked;
    }

    public void win(float speedWin)
    {
        won = true;
        rigidBody.gravityScale = 0;
        gameObject.transform.eulerAngles = LookAt2d(rigidBody.velocity);
        rigidBody.velocity = rigidBody.velocity.normalized*speedWin;
        rigidBody.angularVelocity = 0f;
        spriteRenderer.sprite = winSprite;
        spriteRenderer.flipX = false;
    }

    public void reset(Vector3 initPosition)
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
        gameObject.transform.position = initPosition;
        gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }


    public Vector3 LookAt2d (Vector3 vec)
    {
        return new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, Vector2.SignedAngle(Vector2.up, vec - gameObject.transform.position));
    }
}
