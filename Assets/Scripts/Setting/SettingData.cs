using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Setting
{
	/// <summary>
	/// 세팅 데이터
	/// </summary>
	public class SettingData
	{
		//Sound
		public float bgmValue; //브금 크기
		public float effValue; //효과음 크기

		//Grapic
		public int resolutionIndex; //해상도 인덱스
		public int grapicQualityIndex; //그래픽 퀄리티 인덱스
		public bool isFullScreen; //전체화면 여부
	}
}
