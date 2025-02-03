using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxColorChanger : MonoBehaviour
{
    float step = 0;
    float duration = 5f;
    Color colorStart = Color.white;
    Color colorEnd = Color.black;

    private void Update()
    {
        Debug.Log(step);
        RenderSettings.skybox.SetColor("_Tint", Color.Lerp(colorStart, colorEnd, step));
        step += Time.deltaTime / duration;
    }
}
