using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxBlender : MonoBehaviour
{
    float skyboxBlendFactor = 0;

    GameObject aliPasa;
    Animator aliPasaAnim;

    [SerializeField] Light sceneLight;

    void Start()
    {
        // sunny
        RenderSettings.skybox.SetFloat("_Blend", 0);
        sceneLight.intensity = 1f;


        // get ali pasa animator
        aliPasa = GameObject.FindGameObjectWithTag("Ali Pasa");
        aliPasaAnim = aliPasa.GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(skyboxBlendFactor);
        if (aliPasa != null && aliPasaAnim.GetBool("Scared") == true)
        {
            if (skyboxBlendFactor < 0.9)
            {
                // darker sky
                UpdateSkybox(skyboxBlendFactor);
                skyboxBlendFactor += 0.003f;

                // lower light's intensity
                if (sceneLight.intensity >= 0)
                {
                    sceneLight.intensity -= 0.003f;
                }
            }
        }
        else if (aliPasa == null)
        {
            if (skyboxBlendFactor >= 0)
            {
                // lighter sky
                UpdateSkybox(skyboxBlendFactor);
                skyboxBlendFactor -= 0.003f;
                if (sceneLight.intensity <= 1)
                {
                    sceneLight.intensity += 0.003f;
                }
            }
        }
    }
    void UpdateSkybox(float skyboxBlendFactor)
    {
        RenderSettings.skybox.SetFloat("_Blend", skyboxBlendFactor);
    }
}