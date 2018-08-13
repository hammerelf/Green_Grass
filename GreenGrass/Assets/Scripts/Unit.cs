using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public int health;
    public int attack;
    public string unitName;

    public void Awake()
    {
        health = 5;
        attack = 2;
    }

    //public class Unit
    //{
    //    abstract public void PerformAction();
    //}

    //public class Harvester : Unit
    //{
    //    override public void PerformAction()
    //    {

    //    }
    //}
}
