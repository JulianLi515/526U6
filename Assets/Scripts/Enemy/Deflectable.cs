using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface Deflectable
{
    public Transform dfTransform {  get; }

    protected virtual void OnDeflect(Deflectable df)
    {

    }
    public virtual Transform GetTransform()
    {
        return null;
    }
}
