using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Utill;

namespace Json
{
    public class SaveManager
	{
		private static string _dataPath = Application.persistentDataPath + "/Save/";

		/// <summary>
		/// 유저 데이터 저장
		/// </summary>
		public static void Save<T>(ref T userSaveData)
		{
			string path = _dataPath + typeof(T).FullName + ".txt";

			if (!File.Exists(path))
			{
				Directory.CreateDirectory($"{Application.persistentDataPath}/Save");
			}
			string jsonData = JsonUtility.ToJson(userSaveData);
			File.WriteAllText(path, jsonData);
		}

		/// <summary>
		/// 유저 데이터 불러오기
		/// </summary>
		public static void Load<T>(ref T userSaveData)
		{
			string path = _dataPath + typeof(T).FullName + ".txt";

			if (File.Exists(path))
			{
				string jsonData = File.ReadAllText(path);
				T saveData = JsonUtility.FromJson<T>(jsonData);
				userSaveData = saveData;
			}
		}

		/// <summary>
		/// 세이브한 적이 있는지 체크
		/// </summary>
		/// <returns></returns>
		public static bool GetCheckBool()
		{
			return File.Exists(_dataPath);
		}
	}
}
