using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Ball")
            {
                Destroy(collision.gameObject);
            }
            else if (collision.tag == "Player")
            {
            GameManager.instance.SpawnPlayer();
            Destroy(collision.gameObject);
            }
        }
    
}
