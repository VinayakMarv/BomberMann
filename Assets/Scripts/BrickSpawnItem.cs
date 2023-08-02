using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawnItem : MonoBehaviour
{
    public GameObject FinalGate;
    private void OnDestroy()
    {
        if (GameManager.gameManagerInstance.finalGateSpawned) return;
        GameManager.gameManagerInstance.brickCount--;
        if (Random.Range(0, GameManager.gameManagerInstance.brickCount) <= 1)
        {
            GameManager.gameManagerInstance.finalGateSpawned = true;
            var obj = Instantiate(FinalGate);
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.identity;
        }
    }
}
