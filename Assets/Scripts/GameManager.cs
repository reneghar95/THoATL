using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int currentNight = 1;
    public static bool isVictory = false;

    [Header("HUD - Nights")]
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private GameObject objectiveBed;
    [SerializeField] private GameObject objectiveTarget;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private GameObject narrativePanel;
    [SerializeField] private TextMeshProUGUI narrativeText;

    [Header("UI - Ending Screens")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI subtitleText;

    // Strategy: referencia a la conducta de la noche actual
    private NightBehavior currentBehavior;

    private bool objectiveReached = false;
    private bool waitingForBed = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Escaped" || currentScene == "Trapped")
        {
            SetupEndScreen(currentScene);
            return;
        }

        SetupNight();
    }

    // ─── FACTORY METHOD ──────────────────────────────────

    private NightBehavior CreateBehavior(int night)
    {
        switch (night)
        {
            case 1: return new Night1Behavior();
            case 2: return new Night2Behavior();
            case 3: return new Night3Behavior();
            default: return new Night1Behavior();
        }
    }

    // ─── PANTALLAS DE FIN ────────────────────────────────

    void SetupEndScreen(string sceneName)
    {
        if (titleText == null || subtitleText == null) return;

        if (sceneName == "Escaped")
        {
            titleText.text = "You escaped";
            subtitleText.text = "It's gone. Or you are.";
            AudioManager.Instance.PlayVictoryMusic();
        }
        else
        {
            titleText.text = "You are trapped.";
            subtitleText.text = "Lost in a place that has no size.";
            AudioManager.Instance.PlayDefeatMusic();
        }
    }

    public void RestartGame()
    {
        currentNight = 1;
        isVictory = false;
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.StopAllSFX();
        if (SceneTransition.Instance != null)
            SceneTransition.Instance.LoadScene("MainMenu");
        else
            SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // ─── LÓGICA DE NOCHES ────────────────────────────────

    void SetupNight()
    {
        // Crear la estrategia correspondiente a la noche actual
        currentBehavior = CreateBehavior(currentNight);

        // Las dangerzones solo se activan en la ultima noche (incluye las inactivas para resetearlas)
        DangerZone[] dangerZones = FindObjectsByType<DangerZone>(FindObjectsInactive.Include);
        foreach (DangerZone dz in dangerZones)
        {
            if (currentNight == 3)
                dz.ApplyAnomaly();
            else
                dz.ResetAnomaly();
        }


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            player.transform.position = spawnPosition;

        // El texto inicial lo define cada NightBehavior
        SetObjectiveText(currentBehavior.InitialObjectiveText);

        // BedActiveFromStart determina el estado inicial de las zonas
        ActivateBed(currentBehavior.BedActiveFromStart);
        ActivateTarget(!currentBehavior.BedActiveFromStart);

        // Night3 empieza esperando la cama; Night1 y Night2 no
        waitingForBed = currentBehavior.BedActiveFromStart;
        objectiveReached = false;

        if (narrativePanel != null)
            narrativePanel.SetActive(false);

        AudioManager.Instance.PlayNightMusic(currentNight);
    }

    public void ObjectiveReached(bool isReturn)
    {
        if (isReturn && waitingForBed)
        {
            waitingForBed = false;
            currentBehavior.OnReturnToBed(this);
        }
        else if (!objectiveReached && !isReturn && !waitingForBed)
        {
            objectiveReached = true;
            waitingForBed = true;
            currentBehavior.OnFirstObjectiveReached(this);
        }
    }

    // ─── MÉTODOS PÚBLICOS PARA LAS STRATEGIES ────────────

    public void SetObjectiveText(string text)
    {
        if (objectiveText != null)
            objectiveText.text = text;
    }

    public void ActivateBed(bool state)
    {
        if (objectiveBed != null)
            objectiveBed.SetActive(state);
    }

    public void ActivateTarget(bool state)
    {
        if (objectiveTarget != null)
            objectiveTarget.SetActive(state);
    }

    public void ShowNarrative(string text, float hideDuration, float afterDelay)
    {
        if (narrativePanel == null || narrativeText == null) return;
        narrativeText.text = text;
        narrativePanel.SetActive(true);
        AudioManager.Instance.PlayNarrative();
        Invoke("HideNarrative", hideDuration);
        Invoke("AfterNarrative", afterDelay);
    }

    public void ScheduleNextNight(float delay)
    {
        Invoke("LoadNextNight", delay);
    }

    public void ScheduleVictory(float delay)
    {
        Invoke("TriggerVictory", delay);
    }

    // ─── MÉTODOS PRIVADOS INTERNOS ───────────────────────

    void AfterNarrative()
    {
        SetObjectiveText("Go back to bed.");
        ActivateTarget(false);
        ActivateBed(true);
    }

    void HideNarrative()
    {
        if (narrativePanel != null)
            narrativePanel.SetActive(false);
    }

    public void TriggerVictory()
    {
        isVictory = true;
        AudioManager.Instance.StopDangerZone();
        AudioManager.Instance.StopFootsteps();
        if (SceneTransition.Instance != null)
            SceneTransition.Instance.LoadScene("Escaped");
        else
            SceneManager.LoadScene("Escaped");
    }

    public void TriggerDefeat()
    {
        isVictory = false;
        AudioManager.Instance.StopDangerZone();
        AudioManager.Instance.StopFootsteps();
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySanityDepleted();
        if (SceneTransition.Instance != null)
            SceneTransition.Instance.LoadScene("Trapped");
        else
            SceneManager.LoadScene("Trapped");
    }

    void LoadNextNight()
    {
        currentNight++;
        if (currentNight > 3)
            TriggerVictory();
        else
        {
            if (SceneTransition.Instance != null)
                SceneTransition.Instance.LoadScene("Night" + currentNight);
            else
                SceneManager.LoadScene("Night" + currentNight);
        }
    }
}