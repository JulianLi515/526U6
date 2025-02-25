using UnityEngine;
using UnityEngine.Splines;

public class Eaa : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SplineAnimate aa;
    void Start()
    {
        aa = GetComponent<SplineAnimate>();
        aa.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (aa.IsPlaying)
        {
            Debug.Log("Playing");
        }
    }
}
