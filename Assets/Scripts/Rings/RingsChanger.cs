using UnityEngine;
using Creatures;
public class RingsChanger : MonoBehaviour
{
    [SerializeField] private int _valueToChange;
    public void ChangeRingsValue(GameObject go)
    {
        var silver = go.GetComponent<RingsComponent>();
        if (silver == null) return;
        silver.ApplyRingsChange(_valueToChange);
    }
}
