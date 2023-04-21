using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioSource longSFXSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource sfxSecondarySource;
  
    public static float preferedMusicVolume = -5;
    public static float preferedSFXVolume = -5;


    private Coroutine fadeCoroutine;
    private bool playingMusic;

    public AudioClip click;


    #region Singleton
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        playingMusic = false;



    }

    #endregion

    #region Save Stuff
    private void Start()
    {

    }

    public void UpdateMusicVolume(float volumeAmt)
    {
        // Update the volume of the musicSource and sfxSource based on the value of the volume slider
        musicSource.volume = volumeAmt;
        preferedMusicVolume = volumeAmt;
    }

    public void UpdateSFXVolume(float volumeAmt)
    {
        // Update the volume of the musicSource and sfxSource based on the value of the volume slider
        sfxSource.volume = volumeAmt;
        longSFXSource.volume = volumeAmt;
        sfxSecondarySource.volume = volumeAmt;
        preferedSFXVolume = volumeAmt;
    }

    #endregion


    public void PlaySound(AudioClip clip, float pitch = 1, bool useSecondarySource = false)
    {
        var source = useSecondarySource ? sfxSecondarySource : sfxSource;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        source.PlayOneShot(clip);
        source.pitch = pitch;
       

    }

    public void PlayMusic(AudioClip clip, bool isSFX = false, bool loop = false)
    {
        var source = isSFX ? longSFXSource : musicSource;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        playingMusic = true;

        source.volume = 0;
        source.clip = clip;
        source.loop = loop;
        source.Play();


     
    }

  


    public void StopMusic()
    {
        playingMusic = false;
        musicSource.Stop();
    }

    public void FadeOut(float duration, bool isSFX = false)
    {
        var source = isSFX ? longSFXSource : musicSource;

        playingMusic = false;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeOutCoroutine(duration, source));

    }

    private IEnumerator FadeOutCoroutine(float duration, AudioSource sourceToFade)
    {
        if (playingMusic) { yield break; }

        var startVolume = sourceToFade.volume;

        while (sourceToFade.volume > 0)
        {
            if (playingMusic) { yield break; }

            sourceToFade.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        sourceToFade.Stop();
        sourceToFade.volume = startVolume;
    }

    void StopVfx()
    {
        FadeOut(1, true);
    }



        
    public void PlayClick() => PlaySound(click);
    
}