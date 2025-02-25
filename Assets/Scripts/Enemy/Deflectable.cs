using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface Deflectable
{
    public Transform dfTransform {  get; }
    public bool grabbable {  get; }

    protected virtual void OnDeflect(Deflectable df)
    {

    }
    public virtual Transform GetTransform()
    {
        return null;
    }
    public bool canGrab()
    {
        return false;
    }
}
