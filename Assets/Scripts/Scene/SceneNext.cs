using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Scene
{
    public class SceneNext : MonoBehaviour
    {
        private int _sceneIndex = 1;
        private int _addNumber = 1;

        public int SceneIndex => _sceneIndex;

        public void Next()
        {
            SceneManager.LoadScene(_sceneIndex);
        }

        public void Show()
        {
            _sceneIndex = SceneManager.GetActiveScene().buildIndex + _addNumber;
        }

        public void Open()
        {
            SceneManager.LoadScene(_sceneIndex);
        }

        public void Init(int sceneIndex)
        {
            _sceneIndex = sceneIndex;
        }
    }
}