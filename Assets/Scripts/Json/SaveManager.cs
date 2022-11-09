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
		/// ���� ������ ����
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
		/// ���� ������ �ҷ�����
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
		/// ���̺��� ���� �ִ��� üũ
		/// </summary>
		/// <returns></returns>
		public static bool GetCheckBool()
		{
			return File.Exists(_dataPath);
		}
	}
}
