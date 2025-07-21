using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelController : MonoBehaviour
{
	public GameObject settingPanel;
	public void OnSettingButtonClicked() {
		settingPanel.SetActive(true);
	}
	public void OnExitButtonClicked() {
		settingPanel.SetActive(false);
	}
}
