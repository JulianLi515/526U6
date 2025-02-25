using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface Deflectable
{
    public Transform dfTransform {  get; }
    public bool grabbable {  get; }

    public int id { get; }

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
    public int getID()
    {
        return 0;
    }
}
