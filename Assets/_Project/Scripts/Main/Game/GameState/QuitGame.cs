using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class QuitGame : GameState
        {
            public override async UniTask EnterState()
            {
                await UniTask.Yield();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}