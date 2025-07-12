using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongInfo
{
	private static int curSongnum = 0;
    private static string[] songbasenamesAscii = new string[] {
		@"tabeyo",
		@"madakana",
		// @"遥かな旅路",
		// @"3分間のトキメキ",
		// @"約束の場所へ"
	};
    private static string [] songtitle = new string [] {
		@"らーめんたべよう",
		@"らーめんまだかな",
		// @"遥かな旅路",
		// @"3分間のトキメキ",
		// @"約束の場所へ"
	};
	private static int[] numOfMeasure{ get; } = new int[] {
		72, // @"らーめんたべよう",
		72, // @"らーめんまだかな",
		// -1, // @"遥かな旅路",
		// -1, // @"3分間のトキメキ",
		// -1, // @"約束の場所へ"
	};
	public static bool SetCurSongnum(int num) {
		if (CheckSongNum(num)) {
			curSongnum = num;
			return true;
		} else {
			return false;
		}
	}
	public static int GetCurSongnum() {
		return curSongnum;
	}
	public static int NumOfSongs() {
		return songtitle.Length;
	}
	public static int GetNumOfMeasure() {
		return numOfMeasure[curSongnum];
	}
	public static bool CheckSongNum(int num) {
		if (num < 0) return false;
		if (num >= NumOfSongs()) return false;
		return true;
	}
	public static string GetTitle()
	{
		return songtitle[curSongnum];
	}
	public static string GetBaseNameAscii()
	{
		return songbasenamesAscii[curSongnum];
	}
	public static string GetBaseName()
	{
		return songtitle[curSongnum];
	}
	public static string GetAudioClipName()
	{
		return $"Audio/{GetBaseNameAscii()}";
	}
	public static string GetSMFPath()
	{
		return $"{Application.streamingAssetsPath}/{GetBaseName()}.mid";
	}
	public static string GetInfoPath()
	{
		return $"{Application.streamingAssetsPath}/{GetBaseName()}.json";		
	}
}
