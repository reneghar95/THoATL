public class Night3Behavior : NightBehavior
{
    public override string InitialObjectiveText =>
        "The house fills with shadows. Avoid them and find the exit.";

    // La salida (bed) está activa desde el inicio de Night3
    public override bool BedActiveFromStart => true;


    public override void OnReturnToBed(GameManager gm)
    {
        AudioManager.Instance.PlayExit();
        gm.SetObjectiveText("You escaped...");
        gm.ScheduleVictory(1.5f);
    }
}