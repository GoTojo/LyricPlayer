///
/// Paramter.js
/// Copyright (c) 2025 gotojo.
///
using UnityEngine;

public class Parameter {
	public enum Command {
		Title,
		Line,
		Words,
		MultiL,
		MultiR,
		MultiVL,
		MultiVR,
		MultiWordL,
		MultiWordR,
		MultiWordVL,
		MultiWordVR
	};
	public enum Font {
		JKMaruGothic,
		DelaGothicOne,
		HachiMaruPop,
		KaiseiTokumin,
		LightNovelPOP,
		RocknRollOne
	};
	public static string[] GetOptions(string command, int num) {
		string[] args = command.Split("_");
		switch (args[0]) {
		// LyricControls
		case "Title":
		case "Line":
		case "Words":
		case "MultiL":
		case "MultiR":
		case "MultiVL":
		case "MultiVR":
		case "MultiWordL":
		case "MultiWordR":
		case "MultiWordVL":
		case "MultiWordVR":
			return LyricControl.GetOptions(command, num);
		default:
			break;
		}
		return null;
	} 
};