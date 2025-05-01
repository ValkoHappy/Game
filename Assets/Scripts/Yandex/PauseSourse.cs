using UnityEngine;

public class PauseSourse : MonoBehaviour
{
    public PauseSourse(string key)
    {
        Key = key;
    }

    public string Key { get; private set; }
}
