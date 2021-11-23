using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{

    public BoidBehaviour boid;
    public int boidCount;
    private List<BoidBehaviour> boidList = new List<BoidBehaviour>();
    Vector3 v1, v2, v3;


    [Range(1, 10)]
    public int distanceValue = 5;

    [Range(100, 10)]
    public int speed = 75;

    [Range(1, 15)]
    public int centreOfMass = 10;


    void Start()
    {
        InitializeBoids(boid);
    }

    void Update()
    {
        foreach (BoidBehaviour tempBoid in boidList)
        {
            v1 = Rule1(tempBoid);
            v2 = Rule2(tempBoid);
            v3 = Rule3(tempBoid);
            Debug.Log(v1);

            tempBoid.velocity += (v1 + v2 + v3) / speed;
            //Vector3 tempPosition = tempBoid.GetPosition();
            //tempPosition = tempPosition + tempBoid.velocity;
            tempBoid.OnUpdate();

        }
    }


    private void InitializeBoids(BoidBehaviour prefab)
    {
        for (int i = 0; i < boidCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
            BoidBehaviour tempBoid = Instantiate(prefab, randomPosition, Quaternion.identity);

            tempBoid.name = "boid" + i;
            boidList.Add(tempBoid);
        }
    }

    Vector3 Rule1(BoidBehaviour boidInstance)
    {
        //average position all boids
        //centreOfMass = (all boids) / n

       
        Vector3 percievedCentreOfMass;
        Vector3 positionStorage = new Vector3(0, 0, 0);
        int n = boidList.Count;

        foreach (BoidBehaviour tempBoid in boidList)
        {
            if(tempBoid != boidInstance)
            {
                Vector3 tempPosition = tempBoid.GetPosition();
                positionStorage = positionStorage + tempPosition;
            }
        }
        percievedCentreOfMass = positionStorage / (n - 1);
        //Debug.Log("positionStorage = " + positionStorage);
       // Debug.Log("Centre of mass = " + percievedCentreOfMass);
        Vector3 returnValue = (percievedCentreOfMass - boidInstance.GetPosition()) / centreOfMass;


        return returnValue;
    }

   Vector3 Rule2 (BoidBehaviour boidInstance)
    {

        Vector3 c = new Vector3(0,0,0);
        Vector3 positionBoidInstance = boidInstance.GetPosition();

        foreach(BoidBehaviour tempBoid in boidList)
        {
            if (tempBoid != boidInstance)
            {
                Vector3 tempPosition = tempBoid.GetPosition();
                if (Vector3.Distance(tempPosition, positionBoidInstance) < distanceValue )
                {
                    c = c - (tempPosition - positionBoidInstance);
                }
            }
        }
        //Debug.Log(c);
        return c;
    }

    Vector3 Rule3(BoidBehaviour boidInstance)
    {
        Vector3 percievedVelocity = new Vector3(0,0,0);
        int n = boidList.Count;

        foreach (BoidBehaviour tempBoid in boidList)
        {
            if(tempBoid != boidInstance)
            {
                percievedVelocity = percievedVelocity + tempBoid.velocity;
            }
        }
        percievedVelocity = percievedVelocity / (n - 1);
        Vector3 returnValue = (percievedVelocity - boidInstance.velocity) / 30;
        //Debug.Log(returnValue);
        return returnValue;
    }
}