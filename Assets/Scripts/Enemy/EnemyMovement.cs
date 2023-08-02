using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour,IDie
{
    public int div = 2;
    public LayerMask nonWalk;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movementDirection, velocity;
    private bool hasMovementDirection;
    public float Speed = 5f;
    private bool isKilled=false;
    private int cooldown = 200;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementDirection = Vector2.zero;
        hasMovementDirection = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isKilled)
            return;

        if (!hasMovementDirection || isInMid())
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    movementDirection = Vector2.left;
                    break;

                case 1:
                    movementDirection = Vector2.right;
                    break;

                case 2:
                    movementDirection = Vector2.down;
                    break;

                case 3:
                    movementDirection = Vector2.up;
                    break;
            }
            animator.SetBool("LeftRight", !animator.GetBool("LeftRight"));
            hasMovementDirection = true;
        }

        velocity = Speed * movementDirection;

        if (Physics2D.OverlapBox(rb.position+movementDirection/div, Vector2.one / 2f, 0f, nonWalk))
        {
            velocity = Vector2.zero;
            movementDirection = Vector2.zero;
            hasMovementDirection = false;
        }

        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    private bool isInMid()
    {
        if (cooldown > 0) return false;
        var posX = transform.position.x;
        posX -= (int)posX;
        var posY = transform.position.y;
        posY -= (int)posY;
        if ((posX > 0.95 || posX < 0.05) && (posY > 0.95 || posY < 0.05))
        {
            //animator.SetBool("LeftRight",!animator.GetBool("LeftRight"));
            cooldown = 200; return true;
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (isKilled)
            return;
        cooldown--;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
    public void Die()
    {
        animator.enabled = true;
        animator.SetTrigger("Death");
        isKilled = true;
        //GetComponent<Rigidbody2D>().isKinematic = true;
        StartCoroutine(Dying());
    }
    IEnumerator Dying()
    {
        yield return new WaitForSeconds(0.3f);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || animator.IsInTransition(0))
        {
            yield return null;
        }
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        GameManager.gameManagerInstance.EnemyDie();
    }
}
