
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody characterRB;
    private float maxSpeed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterRB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", characterRB.velocity.magnitude / maxSpeed);
    }
}
