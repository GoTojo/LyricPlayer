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
	public static string [] GetOptions(string command, int num) {
		switch (command) {
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