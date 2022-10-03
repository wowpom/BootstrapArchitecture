using System.Collections;
using UnityEngine;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _loadingCanvas;

        private void Awake()
        {
            _loadingCanvas = GetComponent<CanvasGroup>();
            DontDestroyOnLoad(gameObject);
        }

        public void Show()
        {
            _loadingCanvas.alpha = 1f;
            gameObject.SetActive(true);
        }

        public void Hide()
            => StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (_loadingCanvas.alpha > 0f)
            {
                _loadingCanvas.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}
