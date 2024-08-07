using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Rune.Scripts.Services
{
    public class SceneService
    {
        public async UniTask  ShowMenuScene()
        {
            await SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        }

        public async UniTask ShowGameScene()
        {
            await SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            var targetScene = SceneManager.GetSceneByBuildIndex(1);
            SceneManager.SetActiveScene(targetScene);
        }

        public async UniTask UnloadGameScene()
        {
            await SceneManager.UnloadSceneAsync(1);
        }
    }
}