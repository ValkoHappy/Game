using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStopControl : MonoBehaviour
{
    private HashSet<PauseSourse> _sources = new HashSet<PauseSourse>();

    private int _gameScene = 3;

    public void RemovePauseSourse(PauseSourse source)
    {
        _sources.Remove(_sources.FirstOrDefault(x => x.Key == source.Key));

        if (_sources.Count > 0)
            return;

        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void AddPauseSourse(PauseSourse source)
    {
        _sources.Add(source);

        Time.timeScale = 0;
        AudioListener.pause = true;
    }
}
