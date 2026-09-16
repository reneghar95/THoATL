using UnityEngine;
using System.Collections.Generic;

public class SanityManager : MonoBehaviour
{
    public static SanityManager Instance;

    [SerializeField] private float maxSanity = 100f;
    [SerializeField] private float drainRate = 25f;

    // Read-only para encapsular
    public float CurrentSanity { get; private set; }
    public float MaxSanity => maxSanity;

    private bool isDraining = false;

    // Los observers
    private List<ISanityObserver> observers = new List<ISanityObserver>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CurrentSanity = maxSanity;

        foreach (MonoBehaviour mb in FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude))
        {
            if (mb is ISanityObserver sanityObserver)
                RegisterObserver(sanityObserver);
        }

        SanityUIObserver uiObserver = FindAnyObjectByType<SanityUIObserver>();
        if (uiObserver != null)
            uiObserver.Initialize(maxSanity);

        NotifyObservers();
    }

    void Update()
    {
        if (!isDraining) return;

        CurrentSanity -= drainRate * Time.deltaTime;
        CurrentSanity = Mathf.Clamp(CurrentSanity, 0, maxSanity);

        // Cambio en sanity = notificar a los observers
        NotifyObservers();

        if (CurrentSanity <= 0)
            GameManager.Instance.TriggerDefeat();
    }

    //GESTIÓN DE OBSERVERS

    public void RegisterObserver(ISanityObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void UnregisterObserver(ISanityObserver observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        //Para notificar a cada observer
        foreach (ISanityObserver observer in observers)
            observer.OnSanityChanged(CurrentSanity, maxSanity);
    }

    public void SetDraining(bool state)
    {
        isDraining = state;
    }
}