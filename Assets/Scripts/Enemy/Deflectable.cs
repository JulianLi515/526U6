using UnityEngine;

public interface Deflectable
{
    public Transform dfTransform {  get; set; }
    //public float dfPower { get; set; }

    protected virtual void OnDeflect(Deflectable df)
    {

    }
}
