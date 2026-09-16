using UnityEngine;
using UnityEngine.UI;

// MonoBehaviour para poder asignarse a un GameObject, ISanityObserver para el observer
public class SanityUIObserver : MonoBehaviour, ISanityObserver
{
    [SerializeField] private Slider sanitySlider;

    public void OnSanityChanged(float currentSanity, float maxSanity)
    {
        if (sanitySlider == null) return;
        sanitySlider.value = currentSanity;
    }

    public void Initialize(float maxSanity)
    {
        if (sanitySlider == null) return;
        sanitySlider.maxValue = maxSanity;
        sanitySlider.value = maxSanity;
        sanitySlider.gameObject.SetActive(GameManager.currentNight == 3);
    }
}