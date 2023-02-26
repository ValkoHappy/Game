using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Level;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Level.Side _side;

    public Level.Side Side => _side;
}
