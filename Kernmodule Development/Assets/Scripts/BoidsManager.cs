using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{

    public BoidBehaviour boid;
    public int boidCount;
    private List<BoidBehaviour> boidList = new List<BoidBehaviour>();


    void Start()
    {
        //TODO: put al the boids in a list
        InitializeBoids(boid);
    }

    void Update()
    {
        foreach (BoidBehaviour tempBoid in boidList)
        {
            //Rule1(tempBoid);
            tempBoid.OnUpdate();
        }
    }


    private void InitializeBoids(BoidBehaviour prefab)
    {
        for (int i = 0; i < boidCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
            BoidBehaviour tempBoid = Instantiate(boid, randomPosition, Quaternion.identity);

            tempBoid.name = "boid" + i;
            boidList.Add(tempBoid);
        }
    }

    //Vector3 Rule1(BoidBehaviour boidInstance)
    //{

    //    Vector3 pos = boidScript.GetPosition();
    //    avarage position all boids
    //    foreach (GameObject tempBoid in boidList)
    //    {
    //        positionList.Add(pos);
    //    }

    //    Debug.Log(pos);
    //    centreOfMass = (all boids) / n
    //    return pos;
    //}
}


