using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDirectionHandler : MonoBehaviour
{
    public string animationPrefix;

    private Rigidbody2D rb;
    private Animator animator;

	private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}

	private void FixedUpdate() {
        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y)) {
            animator.Play(animationPrefix + "_Left");
            if (rb.velocity.x > 0) {
                transform.localScale = new Vector3(1, 1);
            }
            else {
                transform.localScale = new Vector3(-1, 1);
            }
        }
        else {
            if (rb.velocity.y > 0) animator.Play(animationPrefix + "_Up");
            else animator.Play(animationPrefix + "_Down");
        }
    }
}
