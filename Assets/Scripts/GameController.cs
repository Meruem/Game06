using UnityEngine;
using Assets.Scripts;

public class GameController : MonoBehaviour
{
    public UnitSelectionController SelectionController;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Right click
        if (Input.GetMouseButtonUp(1))
        {
            SelectionController.SelectedUnits.ForEach(unit =>
            {
                var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var moveComponent = unit.GetComponent<IMovable>();
                if (moveComponent != null)
                {
                    moveComponent.MoveToPosition(targetPos);
                }
            });
        }
    }
}
