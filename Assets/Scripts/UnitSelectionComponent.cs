using System.Collections.Generic;
using Assets.Scripts.Misc;
using Assets.Scripts.Utility;
using UnityEngine;

public class UnitSelectionComponent : MonoBehaviour
{
    private bool _isSelecting;
    private Vector3 _mousePosition1;
    private readonly List<UnitScript> _selectedUnits = new List<UnitScript>();

    public void Update()
    {
        // If we press the left mouse button, save mouse location and begin selection
        if (Input.GetMouseButtonDown(0))
        {
            _isSelecting = true;
            _mousePosition1 = Input.mousePosition;
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {
            if (_isSelecting)
            {
                SelectUnits();
            }
            _isSelecting = false;
        }

        // Right click
        if (Input.GetMouseButtonUp(1))
        {
            _selectedUnits.ForEach(unit =>
            {
                var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                unit.MoveToPosition(targetPos);
            });
        }
    }

    private void SelectUnits()
    {
        _selectedUnits.Clear();
        
        var units = transform.GetComponentsInChildren<UnitScript>();
        units.ForEach(unit =>
        {
            if (IsWithinSelectionBounds(unit.gameObject))
            {
                unit.SelectUnit();
                _selectedUnits.Add(unit);
            }
            else unit.DelesectUnit();
        });
    }


    public bool IsWithinSelectionBounds(GameObject go)
    {
        if (!_isSelecting)
            return false;

        var cam = Camera.main;
        var viewportBounds =
            GUIUtils.GetViewportBounds(cam, _mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(
            cam.WorldToViewportPoint(go.transform.position));
    }

    public void OnGUI()
    {
        if (_isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = GUIUtils.GetScreenRect(_mousePosition1, Input.mousePosition);
            GUIUtils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            GUIUtils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
}