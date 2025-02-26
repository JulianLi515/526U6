using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public interface Deflectable
{
    public Transform dfTransform {  get; }
    public bool grabbable {  get; }
    public int id { get; }
    protected virtual void OnDeflect(Deflectable df){ }
    protected virtual void OnGrab(Deflectable df) { }
    protected virtual void OnSuccess(Deflectable df) { }
    protected virtual void OnFailure(Deflectable df) { }
    public virtual Vector3 getPosition() { return new Vector3(0,0,0);}
    public bool canGrab() { return false; }
    public int getID() { return 0; }
    public bool isDrop() { return true; }
}
