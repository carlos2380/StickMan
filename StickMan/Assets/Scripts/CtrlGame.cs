using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlGame : MonoBehaviour
{
    public Transform finishTraform;
    public CtrlCamera ctrlCamera;
    public GameObject player;
    public GameObject particles;
    public float speedOnWin;
    public CtrlMenu ctrlMenu;

    private Vector3 initPosition;
    private CtrlPlayer ctrlPlayer;
    private bool won;
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

        if (finishTraform.position.x < player.transform.position.x && !won)
        {
            won = true;
            win();
        }
    }

    public void resetLevel()
    {
        ctrlPlayer.reset(initPosition);
    }

    public void win()
    {
        ctrlPlayer.win(speedOnWin);       
        particles.SetActive(true);
        particles.transform.parent = null;
        ctrlCamera.win();
        StartCoroutine(finishLevel());
    }

    IEnumerator finishLevel()
    {
        yield return new WaitForSeconds(3);
        ctrlMenu.goMainMenu();
    } 
}
