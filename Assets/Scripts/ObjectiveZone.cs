using UnityEngine;

// Hereda de interactable para traer el OnTriggerEnter y OnTriggerExit.
public class ObjectiveZone : Interactable
{
    [SerializeField] private bool isReturnToBed = false;
    private bool alreadyTriggered = false;

    protected override void OnPlayerEnter()
    {
        if (alreadyTriggered) return;
        alreadyTriggered = true;
        GameManager.Instance.ObjectiveReached(isReturnToBed);
    }
}