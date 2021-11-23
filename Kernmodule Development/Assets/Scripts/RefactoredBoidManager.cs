//1.Refactor an old piece of code into a new, updated structure.
//2. Follow the agreed code conventions.
//3. Briefly explain the structural changes, and why you chose this approach.
//4. Pull Request this to the central class repository.

//Your work will be peer reviewed by two other students.

using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class RefactoredBoidManager : MonoBehaviour
{
    //in editor variables
    [Range(1, 10)]
    public int distanceValue = 5;
    [Range(10, 200)]
    public int speed = 75;
    [Range(1, 15)]
    public int centreOfMass = 10;
    public int boidCount;

    public int magicNumberInRule3 = 30;

    public BoidBehaviour boid;
    private List<BoidBehaviour> boidList = new List<BoidBehaviour>();
    private Vector3 v1, v2, v3;

    void Start()
    {
        InitializeBoids(boid);
    }

    void Update()
    {
        foreach (BoidBehaviour _tempBoid in boidList)
        {
            v1 = Rule1(_tempBoid);
            v2 = Rule2(_tempBoid);
            v3 = Rule3(_tempBoid);
            Debug.Log(v1);

            _tempBoid.velocity += (v1 + v2 + v3) / speed;
            _tempBoid.OnUpdate();
        }
    }

    //Spawn boids in random value
    private void InitializeBoids(BoidBehaviour _prefab)
    {
        for (int i = 0; i < boidCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
            BoidBehaviour tempBoid = Instantiate(_prefab, randomPosition, Quaternion.identity);

            tempBoid.name = "boid" + i;
            boidList.Add(tempBoid);
        }
    }

    //Calculate the center of mass
    Vector3 Rule1(BoidBehaviour _boidInstance)
    {

        Vector3 percievedCentreOfMass;
        Vector3 positionStorage = new Vector3(0, 0, 0);
        int n = boidList.Count;

        foreach (BoidBehaviour _tempBoid in boidList)
        {
            if (_tempBoid != _boidInstance)
            {
                Vector3 tempPosition = _tempBoid.GetPosition();
                positionStorage = positionStorage + tempPosition;
            }
        }
        percievedCentreOfMass = positionStorage / (n - 1);

        Vector3 returnValue = (percievedCentreOfMass - _boidInstance.GetPosition()) / centreOfMass;
        return returnValue;
    }

    //Calculating distance from other boids
    Vector3 Rule2(BoidBehaviour _boidInstance)
    {

        Vector3 c = new Vector3(0, 0, 0);
        Vector3 positionBoidInstance = _boidInstance.GetPosition();

        //
        foreach (BoidBehaviour _tempBoid in boidList)
        {
            if (_tempBoid != _boidInstance)
            {
                Vector3 tempPosition = _tempBoid.GetPosition();
                if (Vector3.Distance(tempPosition, positionBoidInstance) < distanceValue)
                {
                    c = c - (tempPosition - positionBoidInstance);
                }
            }
        }
        return c;
    }

    //Calculating and changing velocity from boids
    Vector3 Rule3(BoidBehaviour _boidInstance)
    {
        Vector3 percievedVelocity = new Vector3(0, 0, 0);
        int n = boidList.Count;

        foreach (BoidBehaviour _tempBoid in boidList)
        {
            if (_tempBoid != _boidInstance)
            {
                percievedVelocity = percievedVelocity + _tempBoid.velocity;
            }
        }

        percievedVelocity = percievedVelocity / (n - 1);

        Vector3 returnValue = (percievedVelocity - _boidInstance.velocity) / magicNumberInRule3;
        return returnValue;
    }
}