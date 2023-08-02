using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        var death = collision.gameObject.GetComponent<IDie>();
        if (death!=null)
        {
            death.Die();
        }
    }
    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            Destroy(gameObject);
        }
    }

}
