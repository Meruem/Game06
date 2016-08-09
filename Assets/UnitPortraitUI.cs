using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitPortraitUI : MonoBehaviour
{
    public string Name;
    public Text NameText;
	// Use this for initialization

    public void UpdateUI()
    {
        NameText.text = Name;
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
