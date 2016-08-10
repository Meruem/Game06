using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

public class UnitPortraitUI : MonoBehaviour
{
    public string Name;
    public Text NameText;
    public GameObject Unit;
    public UnitSelectionController SelectionController;
	// Use this for initialization

    public void UpdateUI()
    {
        NameText.text = Name;
    }

    public void Select()
    {
        SelectionController.SelectUnit(Unit);
    }

	void Start ()
	{
	    UpdateUI();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
