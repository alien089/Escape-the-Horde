using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyCone : MonoBehaviour
{
    private enemyController enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject.GetComponentInParent<enemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.alerted = true;
            transform.parent.gameObject.SetActive(false);
        }
    }
}
