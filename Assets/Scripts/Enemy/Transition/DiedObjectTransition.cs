public class DiedObjectTransition : EnemyTransition
{
    private void Update()
    {
        if (PeacefulConstruction.IsAlive() == false)
            NeedTransit = true;
    }
}
