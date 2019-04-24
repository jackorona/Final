using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFieldSimulation : MonoBehaviour
{

    private ParticleSystem ps;
    public GameObject Background;
    
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Background == null)
        {           
            var main = ps.main;
            main.simulationSpeed = 100;
        }
    }
} 
       