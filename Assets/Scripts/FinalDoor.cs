using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour,IDie
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.gameManagerInstance.enemyCount <= 0)
        {
            GameManager.gameManagerInstance.Win();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
