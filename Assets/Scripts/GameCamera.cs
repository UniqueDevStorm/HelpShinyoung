using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public float smoothSpeed;
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;
    public float cameraHalfWidth, cameraHalfHeight;
    public float shakePower, shakeTime;
    public bool isCanCameraMove, isCanCameraShake;

    public Camera mainCamera;
    public Transform target;
    public Vector2 offset;
    Vector3 initialPosition;
    Vector3 desiredPosition;

    void Start()
    {
        smoothSpeed = 3f;
        limitMinX = -8.8f;
        limitMaxX = 8.8f;
        limitMinY = -5f;
        limitMaxY = 7f;
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
        shakePower = 0f;
        shakeTime = 0f;
        isCanCameraMove = true;
        isCanCameraShake = false;

        mainCamera = GetComponent<Camera>();
        target = GameObject.Find("ShinYoung").transform;
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isCanCameraShake)
        {
            if (shakeTime > 0f)
            {
                initialPosition = transform.position;

                transform.position = Random.insideUnitSphere * shakePower + initialPosition;
                shakeTime -= Time.deltaTime;
            }
            else
            {
                shakeTime = 0f;
                transform.position = initialPosition;
                isCanCameraShake = false;
            }
        }
    }

    void LateUpdate()
    {
       desiredPosition = new Vector3(
           Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),
           Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight),
           -10);

        if (isCanCameraMove)
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }

    public void VibrateForTime(float power, float time)
    {
        shakePower = power;
        shakeTime = time;
        isCanCameraShake = true;
    }
}
