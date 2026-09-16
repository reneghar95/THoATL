public class Night2Behavior : NightBehavior
{
    public override string InitialObjectiveText =>
        "A strange noise came from the dining room. Investigate.";

    public override void OnFirstObjectiveReached(GameManager gm)
    {
        AudioManager.Instance.PlayDiningRoom();
        // hideDuration: cuánto dura visible el panel
        // afterDelay: cuánto esperar para activar la cama
        gm.ShowNarrative(
            "Did I leave this mess? This house likes to play tricks on me.",
            hideDuration: 5f,
            afterDelay: 7f
        );
    }

    public override void OnReturnToBed(GameManager gm)
    {
        AudioManager.Instance.PlayReturnToBed(2);
        gm.SetObjectiveText("Going back to sleep...");
        gm.ActivateTarget(false);
        gm.ActivateBed(false);
        gm.ScheduleNextNight(3f);
    }
}