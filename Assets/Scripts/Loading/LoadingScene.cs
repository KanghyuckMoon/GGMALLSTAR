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
        public void LoadScene(string sceneName)
        {
            gameObject.SetActive(true);
            SceneManager.LoadScene("LoadingScene");
            SceneManager.sceneLoaded += LoadSceneEnd;
            loadSceneName = sceneName;
            StartCoroutine(Load(sceneName));
        }

        private IEnumerator Load(string sceneName)
        {
            float amount = 0f;
            yield return StartCoroutine(Fade(true));
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;

            float timer = 0.0f;
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