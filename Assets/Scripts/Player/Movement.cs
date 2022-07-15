using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float slidingTime;
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    private float verticalSlideTimer;
    private float horizontalSlideTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // 1 = right | -1 = left
        vertical   = Input.GetAxisRaw("Vertical");   // 1 = up    | -1 = down
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        print(horizontalSlideTimer);
        VerifySlideValues();
        rb.velocity = new Vector2((1*horizontalSlideTimer) * speed, (1*verticalSlideTimer) * speed);
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
        else if (horizontal == 0)
        {
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
        else if (vertical == 0)
        {
            StartCoroutine(VerticalSlidingMovement());
        }
    }

    public IEnumerator HorizontalSlidingMovement()
    {
        while (horizontalSlideTimer != 0)
        {
            if (horizontalSlideTimer < 0)
                horizontalSlideTimer += 0.1f;
            else
                horizontalSlideTimer -= 0.1f;

            if (horizontalSlideTimer < 0 && horizontalSlideTimer > -0.1 || horizontalSlideTimer > 0 && horizontalSlideTimer < 0.1) //used zhen you move from a direction to another one
                horizontalSlideTimer = 0;

            yield return new WaitForSeconds(slidingTime/5);
        }
    }

    public IEnumerator VerticalSlidingMovement()
    {
        while (verticalSlideTimer != 0)
        {
            if (verticalSlideTimer < 0)
                verticalSlideTimer += 0.1f;
            else
                verticalSlideTimer -= 0.1f;

            if (verticalSlideTimer < 0 && verticalSlideTimer > -0.1 || verticalSlideTimer > 0 && verticalSlideTimer < 0.1) //used zhen you move from a direction to another one
                verticalSlideTimer = 0;

            yield return new WaitForSeconds(slidingTime/5);
        }
    }
}
