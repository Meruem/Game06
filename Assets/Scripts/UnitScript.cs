using UnityEngine;
using System.Collections;
using Assets.Scripts.Messages;
using Assets.Scripts.Misc;

public class UnitScript : MonoBehaviour
{
    public float Speed = 5.0f;
    private Vector2 _targetPosition = Vector2.zero;
    private float _minimumDelta = 0.01f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (_targetPosition != Vector2.zero)
	    {
	        var vector = (_targetPosition - (Vector2)transform.position);
	        if (vector.magnitude < _minimumDelta)
	        {
	            _targetPosition = Vector2.zero;
	            return;
	        }
	        
	        transform.position += (Vector3)(vector.normalized)*Time.deltaTime*Speed;
	    }
	}

    public void SelectUnit()
    {
        this.GetPubSub().PublishMessageInContext(new SelectUnitMessage { IsSelected = true});
    }

    public void DelesectUnit()
    {
        this.GetPubSub().PublishMessageInContext(new SelectUnitMessage { IsSelected = false });
    }

    public void MoveToPosition(Vector2 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
