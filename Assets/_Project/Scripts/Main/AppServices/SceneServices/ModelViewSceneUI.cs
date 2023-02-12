using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Main.AppServices.SceneServices
{
    public class ModelViewSceneUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _mainCanvasGroup;
        [SerializeField] private Button _buttonRotateLeft;
        [SerializeField] private Button _buttonRotateRight;
        [SerializeField] private Button _buttonZoomUp;
        [SerializeField] private Button _buttonZoomDown;
        [SerializeField] private Button _buttonChangeAnimation;
        
        private ModelViewSceneControl _viewSceneControl;

        public void Init(ModelViewSceneControl modelViewSceneControl)
        {
            _viewSceneControl = modelViewSceneControl;
            _buttonChangeAnimation.onClick.AddListener(_viewSceneControl.ChangeAnimation);
        }
        
    }
}

