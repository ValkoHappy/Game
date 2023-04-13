using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Level.Side _side;

    public Level.Side Side => _side;
}
