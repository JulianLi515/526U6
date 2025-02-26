using UnityEditor;
using UnityEngine;


public class WeaponsDiction:MonoBehaviour
{
    public GameObject lancePrefab;

    public GameObject getLancePrefab()
    {
        if (lancePrefab == null)
        {
            Debug.LogError("Lance prefab not set");
        }
        return lancePrefab;
    }
}
