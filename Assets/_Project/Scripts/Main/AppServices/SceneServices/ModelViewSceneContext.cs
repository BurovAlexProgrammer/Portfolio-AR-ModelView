using System;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices.SceneServices
{
    public class ModelViewSceneContext : MonoBehaviour
    {
        [SerializeField] private ModelViewSceneControl _sceneControl;
        [SerializeField] private ModelViewSceneUI _sceneUI;

        private void Awake()
        {
            _sceneUI.Init(_sceneControl);
        }
    }
}