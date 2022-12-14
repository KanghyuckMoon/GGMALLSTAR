using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill;

namespace Loading
{
    public class LoadingScene : MonoSingleton<LoadingScene>
    {
        private string loadSceneName;
        public enum LoadingSceneType
		{
            Normal,
            Battle,
            Result,
		}
        public void LoadScene(string sceneName, LoadingSceneType loadingSceneType = LoadingSceneType.Normal)
        {
            gameObject.SetActive(true);
            float delay = 0f;
			switch (loadingSceneType)
			{
                case LoadingSceneType.Normal:
                    delay = 1f;
                    break;
                case LoadingSceneType.Battle:
                    delay = 3f;
                    break;
                case LoadingSceneType.Result:
                    delay = 3f;
                    break;
                default:
					break;
			}
            SceneManager.sceneLoaded += LoadSceneEnd;
            loadSceneName = sceneName;
            StartCoroutine(Load(loadingSceneType, sceneName, delay));
		}

        private IEnumerator LoadLoadingScene(LoadingSceneType loadingSceneType)
        {
            AsyncOperation op;
            switch (loadingSceneType)
            {
                default:
                case LoadingSceneType.Normal:
                    op = SceneManager.LoadSceneAsync("LoadingScene");
                    break;
                case LoadingSceneType.Battle:
                    op = SceneManager.LoadSceneAsync("BattleLoadingScene");
                    break;
                case LoadingSceneType.Result:
                    op = SceneManager.LoadSceneAsync("ResultLoadingScene");
                    break;
            }
            op.allowSceneActivation = true;

            while (!op.isDone)
			{
                yield return null;
            }
            yield break;
        }

		private IEnumerator Load(LoadingSceneType loadingSceneType, string sceneName, float delay = 0f)
        {
            yield return LoadLoadingScene(loadingSceneType);
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;
            float timer = 0.0f - delay;

            while (timer < 1f)
            {
                yield return null;
                timer += Time.unscaledDeltaTime;
            }
            op.allowSceneActivation = true;

            while (!op.isDone)
            {
                yield return null;
            }

        }

        private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == loadSceneName)
            {
                SceneManager.sceneLoaded -= LoadSceneEnd;
            }
        }
	}

}