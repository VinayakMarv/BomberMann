using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour,IDie
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private CircleCollider2D collider;
    private Vector2 direction = Vector2.zero;
    public float speed = 5f;
    private bool isKilled = false;
    [Header("Input")]
    public KeyCode inputUp = KeyCode.UpArrow;
    public KeyCode inputDown = KeyCode.DownArrow;
    public KeyCode inputLeft = KeyCode.LeftArrow;
    public KeyCode inputRight = KeyCode.RightArrow;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collider.isTrigger = true;
            Die();
        }
    }
    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isKilled) return;
        animator.enabled = true;
        if (Input.GetKey(inputUp)) {
            direction = Vector2.up; animator.SetTrigger("Up");
        } else if (Input.GetKey(inputDown)) {
            direction = Vector2.down; animator.SetTrigger("Down");
        } else if (Input.GetKey(inputLeft)) {
            direction = Vector2.left; animator.SetTrigger("Left");
        } else if (Input.GetKey(inputRight)) {
            direction = Vector2.right; animator.SetTrigger("Right");
        } else {
            direction = Vector2.zero;
            animator.enabled = false;
        }

    }

    private void FixedUpdate()
    {
        if (isKilled)
            return;
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
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
        GameManager.gameManagerInstance.Lose();
    }
}
