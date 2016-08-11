using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Misc;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null)
        {
            _isSelecting = true;
            _mousePosition1 = Input.mousePosition;
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0) && EventSystem.current.currentSelectedGameObject == null)
        {
            if (_isSelecting)
            {
                SelectUnits();
            }
            _isSelecting = false;
        }
    }

    private void ClearSelection()
    {
        _selectedUnits.ForEach(unit =>
        {
            var scripts = unit.GetComponentsInChildren<ISelectable>();
            scripts.ForEach(sc => sc.Deselect());
        });
        _selectedUnits.Clear();
        foreach (Transform child in UIParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void SelectUnit(GameObject unit)
    {
        ClearSelection();
        var scripts = unit.GetComponentsInChildren<ISelectable>();
        if (scripts != null)
        {
            scripts.ForEach(sel =>
            {
                sel.Select();
            });
        }
        _selectedUnits.Add(unit);
        CreateUnitPortraitUI(0, unit);
    }

    private void SelectUnits()
    {
        ClearSelection();

        var units = transform.GetComponentsInChildren<ISelectable>();
        units.ForEach(unit =>
        {
            if (IsWithinSelectionBounds(unit.GameObject))
            {
                unit.Select();
                _selectedUnits.Add(unit.GameObject);
            }
        });

        int counter = 0;
        SelectedUnits.ForEach(unit =>
        {
            CreateUnitPortraitUI(counter, unit);
            counter++;
        });

    }

    private void CreateUnitPortraitUI(int position, GameObject unit)
    {
        var unitScript = unit.GetComponent<UnitScript>();

        var uiPortrait = Instantiate(UIPortraitPrefab);
        uiPortrait.transform.SetParent(UIParent, false);
        uiPortrait.transform.localPosition = new Vector3(0, -60*position - 30, 0);
        var script = uiPortrait.GetComponent<UnitPortraitUI>();
        if (script != null)
        {
            script.Unit = unit.gameObject;
            script.Name = unitScript == null ? "Unit" : unitScript.Name;
            script.SelectionController = this;
            script.gameObject.GetComponentInChildren<Button>()
                .onClick.AddListener(() => { script.Select(); });
            script.UpdateUI();
        }
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