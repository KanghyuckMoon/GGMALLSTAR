using System.Collections;
using UnityEngine;
namespace Utill
{
	public class StaticCoroutine : MonoSingleton<StaticCoroutine>
	{
		IEnumerator Perform(IEnumerator coroutine)
		{
			yield return StartCoroutine(coroutine); 
		}
		public static void DoCoroutine(IEnumerator coroutine)
		{         //여기서 인스턴스에 있는 코루틴이 실행될 것이다.        
			Instance.StartCoroutine(Instance.Perform(coroutine));
		}
	}

}