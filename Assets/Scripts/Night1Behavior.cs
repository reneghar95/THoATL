public class Night1Behavior : NightBehavior
{
    public override string InitialObjectiveText =>
        "You awake thirsty. Get some water from the kitchen.";

    public override void OnFirstObjectiveReached(GameManager gm)
    {
        AudioManager.Instance.PlayKitchen();
        gm.SetObjectiveText("Go back to bed.");
        gm.ActivateTarget(false);
        gm.ActivateBed(true);
    }

    public override void OnReturnToBed(GameManager gm)
    {
        AudioManager.Instance.PlayReturnToBed(1);
        gm.SetObjectiveText("Going back to sleep...");
        gm.ActivateTarget(false);
        gm.ActivateBed(false);
        gm.ScheduleNextNight(3f);
    }
}