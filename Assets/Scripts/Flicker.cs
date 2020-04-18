using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class Flicker : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float min = 0.25f;
    [Range(0.0f, 1.0f)]
    public float max = 0.75f;
    private Light2D m_Light;
    // Start is called before the first frame update
    void Start()
    {
        m_Light = GetComponent<Light2D>();
    }

    public static float NextGaussian() 
    {
        float v1, v2, s;
        do
        {
            v1 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            v2 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        } while (s >= 1.0f || s == 0f);

        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

        return v1 * s;
    }

    // Update is called once per frame
    void Update()
    {
        // Hacky random normal thingy
        float random = NextGaussian() / 8 + 1.0f;
        m_Light.intensity = Mathf.Clamp01(min + (max - min) * random);
    }
}
