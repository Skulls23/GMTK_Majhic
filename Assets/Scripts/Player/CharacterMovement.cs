using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float slidingTime = 0.5f;
    private Rigidbody2D rb;
    private UnitStats unitStats;

    private float horizontal;
    private float vertical;

    private float horizontalSlideTimer;
    private float verticalSlideTimer;

    private bool isHorizontalCoroutineRunning = false;
    private bool isVerticalCoroutineRunning = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        unitStats = GetComponent<UnitStats>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // 1 = right | -1 = left
        vertical   = Input.GetAxisRaw("Vertical");   // 1 = up    | -1 = down
    }

    void FixedUpdate()
    {
        VerifySlideValues();
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        //Player can't go outside camera
        if (screenPos.x < Screen.width && screenPos.x > 0 && screenPos.y < Screen.height && screenPos.y > 0)
            rb.velocity = new Vector2(1 * horizontalSlideTimer * unitStats.MaximumMoveSpeed, 1 * verticalSlideTimer * unitStats.MaximumMoveSpeed);
        else
        {
            rb.velocity = Vector3.zero;
            if(screenPos.x >= Screen.width)
                transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y, 0);
            else if (screenPos.x <= 0)
                transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, 0);
            else if (screenPos.y >= Screen.height)
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0);
            else if (screenPos.y >= 0)
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, 0);
        }

    }

    public void VerifySlideValues()
    {
        //Horizontal Sliding
        if (horizontalSlideTimer != 1 && horizontal == 1)
            horizontalSlideTimer = 1;
        else if (horizontalSlideTimer != 1 && horizontal == -1)
            horizontalSlideTimer = -1;
        else if (horizontal == 0 && horizontalSlideTimer != 0 && !isHorizontalCoroutineRunning)
            StartCoroutine(HorizontalSlidingMovement());

        //Vertical Sliding
        if (verticalSlideTimer != 1 && vertical == 1)
            verticalSlideTimer = 1;
        else if (verticalSlideTimer != 1 && vertical == -1)
            verticalSlideTimer = -1;
        else if (vertical == 0 && verticalSlideTimer != 0 &&!isVerticalCoroutineRunning)
            StartCoroutine(VerticalSlidingMovement());
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

    /////////////////////////
    /// GETTERS & SETTERS ///
    /////////////////////////

    public float SlidingTime
    {
        get { return slidingTime; }
        set { slidingTime = value; }
    }
}
