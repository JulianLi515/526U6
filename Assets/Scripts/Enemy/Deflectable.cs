using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface Deflectable
{
    public Transform dfTransform {  get; }
    public bool grabbable {  get; }
    public int id { get; }
    protected virtual void OnDeflect(Deflectable df){ }
    protected virtual void OnGrab(Deflectable df) { }
    protected virtual void OnSuccess(Deflectable df) { }
    protected virtual void OnFailure(Deflectable df) { }
    public virtual Transform GetTransform() { return null;}
    public bool canGrab() { return false; }
    public int getID() { return 0; }
}
