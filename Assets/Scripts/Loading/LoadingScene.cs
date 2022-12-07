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
                    SceneManager.LoadScene("LoadingScene");
                    break;
                case LoadingSceneType.Battle:
                    SceneManager.LoadScene("BattleLoadingScene");
                    delay = 3f;
                    break;
                case LoadingSceneType.Result:
                    SceneManager.LoadScene("ResultLoadingScene");
                    delay = 3f;
                    break;
                default:
					break;
			}
            SceneManager.sceneLoaded += LoadSceneEnd;
            loadSceneName = sceneName;
            StartCoroutine(Load(sceneName, delay));
		}

		private IEnumerator Load(string sceneName, float delay = 0f)
        {
            float amount = 0f;
            yield return StartCoroutine(Fade(true));
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;

            float timer = 0.0f - delay;
            while (!op.isDone)
            {
                yield return null;
                timer += Time.unscaledDeltaTime;

                if (op.progress < 0.9f)
                {
                    amount = Mathf.Lerp(amount, op.progress, timer);
                    if (amount >= op.progress)
                    {
                        timer = 0f;
                    }
                }
                else
                {
                    amount = Mathf.Lerp(amount, 1f, timer);

                    if (amount == 1.0f)
                    {
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }

        private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == loadSceneName)
            {
                SceneManager.sceneLoaded -= LoadSceneEnd;
            }
        }

        private IEnumerator Fade(bool isFadeIn)
        {
            float timer = 0f;

            while (timer <= 1f)
            {
                yield return null;
                timer += Time.unscaledDeltaTime * 2f;
            }

        }
	}

}