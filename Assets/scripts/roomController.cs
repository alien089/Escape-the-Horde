using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomController : MonoBehaviour
{
    private GameObject[] enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for(int i = 0; i < enemy.Length; i++)
            {
                enemy[i].gameObject.GetComponent<enemyController>().activated = true;
            }
        }
    }
}
