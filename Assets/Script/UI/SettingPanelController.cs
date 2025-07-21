using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SettingPanelController : MonoBehaviour
{
	public GameObject settingPanel;
	public TMP_Dropdown edititem;
	public TMP_Dropdown fontSelector;
	public TMP_InputField xinput;
	public TMP_InputField yinput;
	public TMP_InputField sampleText;
	public TitleControl titleControl;
	public LyricGenUnder1Line line;
	public SimpleLyricGen words;
	public LyricGenMultiLine multiL;
	public LyricGenMultiLine multiR;
	public LyricGenMultiLine multiVL;
	public LyricGenMultiLine multiVR;
	public LyricGenMultiLineByWord multiWordL;
	public LyricGenMultiLineByWord multiWordR;
	public LyricGenMultiLineByWord multiWordVL;
	public LyricGenMultiLineByWord multiWordVR;
	private string [] controlType;
	private string [] fontType;
	private LyricBase targetLyric;
	void Awake() {
		controlType = Enum.GetNames(typeof(LyricControl.Type));
		for (var i = 0; i < controlType.Length; i++) {
			TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(controlType[i]);
			edititem.options.Add(optionData);
		}
		fontType = Enum.GetNames(typeof(Parameter.Font));
		for (var i = 0; i < fontType.Length; i++) {
			TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(fontType[i]);
			fontSelector.options.Add(optionData);
		}
		sampleText.text = "さんぷるてきすと";
	}
	public void GetParams() {
		switch (controlType[edititem.value]) {
		case "Title":
			targetLyric = titleControl;
			break;
		case "Line":
			targetLyric = line;
			break;
		case "Words":
			targetLyric = words;
			break;
		case "MultiL":
			targetLyric = multiL;
			break;
		case "MultiR":
			targetLyric = multiR;
			break;
		case "MultiVL":
			targetLyric = multiVL;
			break;
		case "MultiVR":
			targetLyric = multiVR;
			break;
		case "MultiWordL":
			targetLyric = multiWordL;
			break;
		case "MultiWordR":
			targetLyric = multiWordR;
			break;
		case "MultiWordVL":
			targetLyric = multiWordVL;
			break;
		case "MultiWordVR":	
			targetLyric = multiWordVR;
			break;
		default:
			targetLyric = null;
			return;
		}
	}
	public void OnSettingButtonClicked() {
		GetParams();
		settingPanel.SetActive(true);
	}
	public void OnExitButtonClicked() {
		settingPanel.SetActive(false);
	}
	public void OnEditItemChanged(int num) {

	}
	public void OnFontSelectChanged(int num) {

	}
	public void OnInputEndX(string text) {
		
	}
	public void OnInputEndY(string text) {

	}
	public void OnInputEndSampleText(string text) {

	}
}
