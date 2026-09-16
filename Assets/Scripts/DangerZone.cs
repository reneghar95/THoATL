using UnityEngine;

// Hereda de interactable para detectar el player y de IAnomalous para controlar activacion
public class DangerZone : Interactable, IAnomalous
{
    private Animator anim;

    // IAnomalous — descripción de la anomalía
    public string AnomalyDescription => "The shadows have taken form.";

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //parte del Interactable
    protected override void OnPlayerEnter()
    {
        SanityManager.Instance.SetDraining(true);
        AudioManager.Instance.StartDangerZone();
        if (anim != null)
            anim.SetBool("playerNear", true);
    }

    protected override void OnPlayerExit()
    {
        SanityManager.Instance.SetDraining(false);
        AudioManager.Instance.StopDangerZone();
        if (anim != null)
            anim.SetBool("playerNear", false);
    }
    //parte del IAnomalous

    public void ApplyAnomaly()
    {
        gameObject.SetActive(true);
    }

    public void ResetAnomaly()
    {
        gameObject.SetActive(false);
    }
}