using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Misc;
using Assets.Scripts.Utility;
using UnityEngine;

public class UnitSelectionController : MonoBehaviour
{
    private bool _isSelecting;
    private Vector3 _mousePosition1;
    private readonly List<GameObject> _selectedUnits = new List<GameObject>();

    public GameObject UIPortraitPrefab;
    public Transform UIParent;

    public List<GameObject> SelectedUnits {  get { return _selectedUnits; } } 

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

    }

    private void SelectUnits()
    {
        _selectedUnits.Clear();
        
        var units = transform.GetComponentsInChildren<ISelectable>();
        units.ForEach(unit =>
        {
            if (IsWithinSelectionBounds(unit.GameObject))
            {
                unit.Select();
                _selectedUnits.Add(unit.GameObject);
            }
            else unit.Deselect();
        });

        SelectedUnits.ForEach(unit =>
        {
            var uiPortrait = Instantiate(UIPortraitPrefab);
            uiPortrait.transform.SetParent(UIParent);
            var script = uiPortrait.GetComponent<UnitPortraitUI>();
            if (script != null)
            {
                script.Name = "test";
                script.UpdateUI();
            }
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