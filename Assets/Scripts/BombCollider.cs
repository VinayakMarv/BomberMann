using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollider : MonoBehaviour
{
    private CircleCollider2D collider;
    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collider.isTrigger=false;
    }
}
