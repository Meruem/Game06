using UnityEngine;
using System.Collections;
using Assets.Scripts.Messages;
using Assets.Scripts.Misc;

public class SelectionScript : MonoBehaviour
{
    private SpriteRenderer _renderer;

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<SpriteRenderer>();
	    this.GetPubSub().SubscribeInContext<SelectUnitMessage>(m => OnSelectionChange((SelectUnitMessage)m));
	}

    private void OnSelectionChange(SelectUnitMessage selectUnitMessage)
    {
        _renderer.enabled = selectUnitMessage.IsSelected;
    }
}
