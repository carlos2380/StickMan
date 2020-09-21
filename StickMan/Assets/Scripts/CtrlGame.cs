using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlGame : MonoBehaviour
{
    public Transform finishTraform;
    public CtrlCamera ctrlCamera;
    public GameObject player;
    public Vector3 initPosition;
    private CtrlPlayer ctrlPlayer;

    void Start()
    {
        ctrlPlayer = player.GetComponent<CtrlPlayer>();
        initPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(ctrlPlayer.getSticked() == false)
        {
            if(player.transform.position.x < -4.3)
            {
                resetLevel();
            }
            if (player.transform.position.y < -4.6)
            {
                resetLevel();
            }
        }

        if (finishTraform.position.x < player.transform.position.x)
        {
            //win
        }
    }

    public void resetLevel()
    {
        Rigidbody2D rg2d = player.GetComponent<Rigidbody2D>(); 
        rg2d.velocity = Vector2.zero;
        rg2d.angularVelocity = 0f;
        player.transform.position = initPosition;
        player.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }
}
