using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip night1Music;
    [SerializeField] private AudioClip night2Music;
    [SerializeField] private AudioClip night3Music;

    [Header("UI")]
    [SerializeField] private AudioClip startButtonSFX;

    [Header("Player")]
    [SerializeField] private AudioClip footstepSFX;
    private AudioSource footstepSource;

    [Header("Night 1")]
    [SerializeField] private AudioClip kitchenSFX;
    [SerializeField] private AudioClip returnToBedSFX;

    [Header("Night 2")]
    [SerializeField] private AudioClip diningRoomSFX;
    [SerializeField] private AudioClip narrativeSFX;
    [SerializeField] private AudioClip returnToBedNight2SFX;

    [Header("Night 3")]
    [SerializeField] private AudioClip dangerZoneSFX;
    [SerializeField] private AudioClip exitSFX;
    [SerializeField] private AudioClip sanityDepletedSFX;
    [SerializeField] private AudioClip lowSanityMusic;

    [Header("Ending Screens")]
    [SerializeField] private AudioClip victoryMusic;
    [SerializeField] private AudioClip defeatMusic;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private AudioSource dangerZoneSource;


    void Awake()
    {
        // Singleton persistente entre escenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetupSources()
    {
        // Fuente para música (loop)
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.3f;

        // Fuente para SFX (no loop)
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.volume = 0.6f;

        // Fuente dedicada para pasos
        footstepSource = gameObject.AddComponent<AudioSource>();
        footstepSource.loop = true;
        footstepSource.clip = footstepSFX;

        // Fuente dedicada para DangerZone
        dangerZoneSource = gameObject.AddComponent<AudioSource>();
        dangerZoneSource.loop = true;
        dangerZoneSource.clip = dangerZoneSFX;
    }

    // ─── MÚSICA ──────────────────────────────────────────

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayNightMusic(int night)
    {
        switch (night)
        {
            case 1: PlayMusic(night1Music); break;
            case 2: PlayMusic(night2Music); break;
            case 3: PlayMusic(night3Music); break;
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // ─── SFX ─────────────────────────────────────────────

    public void PlayStartButton() => PlaySFX(startButtonSFX);
    public void PlayKitchen() => PlaySFX(kitchenSFX);
    public void PlayReturnToBed(int night)
    {
        if (night == 1) PlaySFX(returnToBedSFX);
        else PlaySFX(returnToBedNight2SFX);
    }
    public void PlayDiningRoom() => PlaySFX(diningRoomSFX);
    public void PlayNarrative() => PlaySFX(narrativeSFX);
    public void PlayDangerZone() => PlaySFX(dangerZoneSFX);
    public void PlayExit() => PlaySFX(exitSFX);
    public void PlaySanityDepleted() => PlaySFX(sanityDepletedSFX);

    public void StopAllSFX()
    {
        sfxSource.Stop();
        footstepSource.Stop();
        dangerZoneSource.Stop();
    }
    public void StartFootsteps()
    {
        if (footstepSFX == null) return;
        if (!footstepSource.isPlaying)
            footstepSource.Play();
    }

    public void StopFootsteps()
    {
        if (footstepSource.isPlaying)
            footstepSource.Stop();
    }
    public void StartDangerZone()
    {
        if (dangerZoneSFX == null) return;
        if (!dangerZoneSource.isPlaying)
            dangerZoneSource.Play();
    }

    public void StopDangerZone()
    {
        if (dangerZoneSource.isPlaying)
            dangerZoneSource.Stop();
    }
    public void PlayVictoryMusic()
    {
        if (victoryMusic == null) return;
        StopMusic();
        sfxSource.PlayOneShot(victoryMusic);
    }
    public void PlayDefeatMusic()
    {
        if (defeatMusic == null) return;
        StopMusic();
        sfxSource.PlayOneShot(defeatMusic);
    }
    public void PlayLowSanityMusic()
    {
        PlayMusic(lowSanityMusic);
    }
    void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }
}