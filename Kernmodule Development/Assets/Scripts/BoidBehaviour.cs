﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{
    public Vector3 velocity;
    public float rotationSpeed;

    public void Start()
    {
        //give random velocity
        velocity = Random.insideUnitSphere * 1;
    }

    public void OnUpdate()
    {
        //* Time.deltaTime -> Else it'll move too fast
        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(velocity), rotationSpeed * Time.deltaTime);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
