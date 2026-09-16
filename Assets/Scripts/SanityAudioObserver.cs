using UnityEngine;

public class SanityAudioObserver : MonoBehaviour, ISanityObserver
{
    // Umbral en porcentaje a partir del cual la música cambia
    [SerializeField] private float lowSanityThreshold = 0.3f;
    private bool isPlayingLowSanityMusic = false;

    public void OnSanityChanged(float currentSanity, float maxSanity)
    {
        // Solo en Night3 (no hay musica de tension en las primeras dos noches)
        if (GameManager.currentNight < 3) return;

        float ratio = currentSanity / maxSanity;

        if (ratio <= lowSanityThreshold && !isPlayingLowSanityMusic)
        {
            isPlayingLowSanityMusic = true;
            AudioManager.Instance.PlayLowSanityMusic();
        }
        else if (ratio > lowSanityThreshold && isPlayingLowSanityMusic)
        {
            isPlayingLowSanityMusic = false;
            AudioManager.Instance.PlayNightMusic(3);
        }
    }
}