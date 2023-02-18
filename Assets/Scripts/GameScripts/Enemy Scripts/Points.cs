using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to give points when the enemy dies
public class Points : MonoBehaviour
{
    [SerializeField] private int deathPoints;

    public int DeathPoints { get => deathPoints; }
}
