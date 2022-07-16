using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    // /!\ To use this script just use ScreenShakeController.Instance.StartShake(the lenght of the shaking, the power of the shaking, the amount of rotation)


    public static ScreenShakeController Instance;

    //Some variable to change the shaking
    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;

    //The position of the camera at the begining of the game
    private Vector3 basePosition;

    // All the variable needed to setup the script
    // public float lenght, power

    //The power of rotation, needed here to fade with time
    private float rotationMultiplier= 15f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // How to use the function
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    StartShake(lenght, power);
        //}
    }

    private void LateUpdate()
    {
        //The function who fade the shaking with time until it stops completely
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime*Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }
        else transform.position = basePosition;

        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }

    //The function to call which start a shaking while the LateUpdate stop it gradually
    public void StartShake(float lenght, float power, float rotation)
    {
        shakeTimeRemaining = lenght;
        shakePower = power;

        shakeFadeTime = power / lenght;

        rotationMultiplier = rotation;

        shakeRotation = power * rotation;
    }
}
