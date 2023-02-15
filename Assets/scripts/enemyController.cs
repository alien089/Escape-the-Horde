using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemyController : MonoBehaviour, IEnemy
{
    private GameObject coneView;
    private PlayerController player;
    private Animator anim;
    private NavMeshAgent agent;
    public float speed = 20f;
    public float time = 0f;
    private bool waiting = true;
    public float startRound = 0f;
    public bool alerted = false;
    public bool activated = false;

    private void Start()
    {
        coneView = transform.GetChild(0).gameObject;
        coneView.transform.Rotate(0, 0, startRound);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        //agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (activated)
        {
            if (!player.isStealth || alerted)
            {
                //agent.destination = player.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (speed / 100));
                anim.SetBool("isMoving", true);
                coneView.gameObject.SetActive(false);
            }
        }
        
        if(waiting)
            StartCoroutine(changeSector());
    }

    private IEnumerator changeSector()
    {
        waiting = false;
        coneView.transform.Rotate(0, 0, -90);
        yield return new WaitForSeconds(time);
        waiting = true;
    }

    private IEnumerator waitTime(float time) 
    {
        yield return new WaitForSeconds(time);
    }

    public void RespawnPlayer(Transform respawnPoint, GameObject player)
    {
        SceneManager.LoadScene("livello 2");
    }
}

public interface IEnemy
{
    public abstract void RespawnPlayer(Transform respawnPoint, GameObject player);
}
