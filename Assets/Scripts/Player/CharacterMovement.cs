using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float slidingTime;
    private Rigidbody2D rb;

    private float horizontal;
    private float vertical;

    private float horizontalSlideTimer;
    private float verticalSlideTimer;

    private bool isHorizontalCoroutineRunning = false;
    private bool isVerticalCoroutineRunning = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // 1 = right | -1 = left
        vertical   = Input.GetAxisRaw("Vertical");   // 1 = up    | -1 = down
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        VerifySlideValues();
        rb.velocity = new Vector2(1 * horizontalSlideTimer * speed, 1 * verticalSlideTimer * speed);
    }

    public void VerifySlideValues()
    {
        //Horizontal Sliding
        if (horizontalSlideTimer != 1 && horizontal == 1)
        {
            horizontalSlideTimer = 1;
        }
        else if (horizontalSlideTimer != 1 && horizontal == -1)
        {
            horizontalSlideTimer = -1;
        }
        else if (horizontal == 0 && horizontalSlideTimer != 0 && !isHorizontalCoroutineRunning)
        {
            print("here");
            StartCoroutine(HorizontalSlidingMovement());
        }

        //Vertical Sliding
        if (verticalSlideTimer != 1 && vertical == 1)
        {
            verticalSlideTimer = 1;
        }
        else if (verticalSlideTimer != 1 && vertical == -1)
        {
            verticalSlideTimer = -1;
        }
        else if (vertical == 0 && verticalSlideTimer != 0 &&!isVerticalCoroutineRunning)
        {
            print("here2");
            StartCoroutine(VerticalSlidingMovement());
        }
    }

    public IEnumerator HorizontalSlidingMovement()
    {
        isHorizontalCoroutineRunning = true;
        while (horizontalSlideTimer != 0)
        {
            if (horizontalSlideTimer < 0)
                horizontalSlideTimer += 0.1f;
            else
                horizontalSlideTimer -= 0.1f;

            if (horizontalSlideTimer < 0 && horizontalSlideTimer > -0.1 || horizontalSlideTimer > 0 && horizontalSlideTimer < 0.1) //used zhen you move from a direction to another one
                horizontalSlideTimer = 0;

            yield return new WaitForSeconds(slidingTime/10);
        }
        isHorizontalCoroutineRunning = false;
    }

    public IEnumerator VerticalSlidingMovement()
    {
        isVerticalCoroutineRunning = true;
        while (verticalSlideTimer != 0)
        {
            if (verticalSlideTimer < 0)
                verticalSlideTimer += 0.1f;
            else
                verticalSlideTimer -= 0.1f;

            if (verticalSlideTimer < 0 && verticalSlideTimer > -0.1 || verticalSlideTimer > 0 && verticalSlideTimer < 0.1) //used zhen you move from a direction to another one
                verticalSlideTimer = 0;

            yield return new WaitForSeconds(slidingTime/10);
        }
        isVerticalCoroutineRunning = false;
    }
}
