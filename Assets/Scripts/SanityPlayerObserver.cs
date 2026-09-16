using UnityEngine;

public class SanityPlayerObserver : MonoBehaviour, ISanityObserver
{
    [SerializeField] private PlayerMovement playerMovement;

    // Velocidad mínima permitida
    [SerializeField] private float minSpeedMultiplier = 0.4f;

    public void OnSanityChanged(float currentSanity, float maxSanity)
    {
        if (playerMovement == null) return;

        // ratio va de 1.0 (cordura llena) a 0.0 (sin cordura)
        float ratio = currentSanity / maxSanity;

        // Interpola entre velocidad mínima y velocidad normal
        // Lerp(min, max, t): cuando ratio=1 → speed normal, cuando ratio=0 → speed mínima
        float speedMultiplier = Mathf.Lerp(minSpeedMultiplier, 1f, ratio);
        playerMovement.SetSpeedMultiplier(speedMultiplier);
    }
}