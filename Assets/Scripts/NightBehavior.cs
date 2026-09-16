
public abstract class NightBehavior
{
    public abstract string InitialObjectiveText { get; }

    public virtual bool BedActiveFromStart => false;


    public virtual void OnFirstObjectiveReached(GameManager gm) { }

    public abstract void OnReturnToBed(GameManager gm);
}