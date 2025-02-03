using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Earthquake : MonoBehaviour
{
    GameObject aliPasa;
    Animator aliPasaAnim;

    [SerializeField] Animator tymfiDragon;
    Animator tymfiDragonAnim;
    AnimatorClipInfo[] currentTymfiDragonAnimInfo;
    string tymfiDragonAnimName;

    [SerializeField] GameObject smolikasDragon;
    Animator smolikasDragonAnim;
    AnimatorClipInfo[] currentSmolikasDragonAnimInfo;
    string smolikasDragonAnimName;

    AudioSource earthquakeAudioSource;

    CameraShakeInstance shake;
    
    public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
    }

    void Start()
    {
        // get ali pasa animator
        aliPasa = GameObject.FindGameObjectWithTag("Ali Pasa");
        aliPasaAnim = aliPasa.GetComponent<Animator>();
        
        // earthquake sound
        earthquakeAudioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        // if people are scared by the dragon
        if (aliPasaAnim.GetBool("Scared") == true)
        {
            StartCoroutine(FadeIn(earthquakeAudioSource, 3f));
            // camera shake
            CameraShaker.Instance.ShakeOnce(4f, 2f, 30f, 30f);
            StartCoroutine(FadeOut(earthquakeAudioSource, 3f));
            enabled = false;
        }       

        // if smolikas dragon is activated then soon the fight will start 
        // so we need the animators of the dragon to shake the camera when needed
        if (smolikasDragon != null && smolikasDragon.activeSelf == true)
        {
            tymfiDragonAnim = tymfiDragon.GetComponent<Animator>();
            currentTymfiDragonAnimInfo = this.tymfiDragonAnim.GetCurrentAnimatorClipInfo(0);
            tymfiDragonAnimName = currentTymfiDragonAnimInfo[0].clip.name;

            smolikasDragonAnim = smolikasDragon.GetComponent<Animator>();
            currentSmolikasDragonAnimInfo = this.smolikasDragonAnim.GetCurrentAnimatorClipInfo(0);
            smolikasDragonAnimName = currentSmolikasDragonAnimInfo[0].clip.name;

            if (smolikasDragonAnimName.Contains("Fist") || tymfiDragonAnimName.Contains("Fist"))
            {
                CameraShaker.Instance.ShakeOnce(2f, 1f, 3f, 1f);
            }

        }

    }
}
