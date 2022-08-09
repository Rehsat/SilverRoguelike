using UnityEngine;
using System;

public class GrabActions : MonoBehaviour
{
    [SerializeField] private GrabPosition[] _grabPositions;
    [SerializeField] private Transform _throwPosition;

    private int _selectedGrabPositionId;
    public void Grab(Grabable objectToGrab)
    {

        var emptyPosition = FindEmptyGrabPosition();
        if (emptyPosition == null) return;
        SetGravityState(objectToGrab.gameObject, false);
        objectToGrab.SetTarget(emptyPosition.Position);
        emptyPosition.TakenGameObject = objectToGrab.gameObject;
    }
    private void SetGravityState(GameObject go, bool state)
    {
        var rigidbody = go.GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = !state;
        var collider = go.GetComponent<Collider2D>();
        collider.enabled = state;
    }
    private GrabPosition FindEmptyGrabPosition()
    {
        foreach (var grabPosition in _grabPositions)
        {
            if (grabPosition.TakenGameObject != null) continue;
            
            return grabPosition;
        }
        return null;
    }
    public void Throw(float power)
    {
        var selectedGrabPosition = _grabPositions[0];
        var throwObject = selectedGrabPosition.TakenGameObject;
        if(throwObject == null)
        {
           selectedGrabPosition = _grabPositions[1];
           throwObject = selectedGrabPosition.TakenGameObject;
        }
        SetGravityState(throwObject, true);
        throwObject.transform.position = _throwPosition.position;
        throwObject.GetComponent<Grabable>().SetTarget(null);

        var rigidbody = throwObject.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.right * power * transform.lossyScale, ForceMode2D.Impulse);

        if (_selectedGrabPositionId < _grabPositions.Length-1) _selectedGrabPositionId++;
        else _selectedGrabPositionId = 0;

        selectedGrabPosition.TakenGameObject = null;
    }
}
[Serializable]
public class GrabPosition
{
    [SerializeField] private Transform _transform;
    public GameObject TakenGameObject;
    public Transform Position => _transform;
}
