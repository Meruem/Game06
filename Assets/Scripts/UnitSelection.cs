using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Messages;
using Assets.Scripts.Misc;

public class UnitSelection : MonoBehaviour, ISelectable
{
    private SpriteRenderer _renderer;

	// Use this for initialization
    public void Start ()
	{
	    _renderer = GetComponent<SpriteRenderer>();
	    this.GetPubSub().SubscribeInContext<SelectUnitMessage>(m => OnSelectionChange((SelectUnitMessage)m));
	}

    private void OnSelectionChange(SelectUnitMessage selectUnitMessage)
    {
        _renderer.enabled = selectUnitMessage.IsSelected;
    }

    public void Select()
    {
        _renderer.enabled = true;
    }

    public void Deselect()
    {
        _renderer.enabled = false;
    }

    public GameObject GameObject { get { return gameObject.transform.parent.gameObject;} }
}
