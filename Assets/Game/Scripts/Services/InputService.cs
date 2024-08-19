using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer.Unity;

namespace Rune.Scripts.Services
{
    public class InputData
    {
        public float Horizontal;
        public float Vertical;
        public Vector2 Direction;
    }
    
    public class InputService : IFixedTickable
    {
        private UnityEvent<InputData> _inputEvent = new UnityEvent<InputData>();
        private Joystick _joystick;
        private InputData _inputData = new InputData();
        
        public InputService(Joystick joystick)
        {
            _joystick = joystick;
        }

        public void AddListener(UnityAction<InputData> unityAction)
        {
            _inputEvent.AddListener(unityAction);
        }

        public void RemoveListener(UnityAction<InputData> unityAction)
        {
            _inputEvent.RemoveListener(unityAction);
        }
        
        public void FixedTick()
        {
           _inputData.Vertical = _joystick.Vertical;
           _inputData.Horizontal = _joystick.Horizontal;
           _inputData.Direction = _joystick.Direction;
           
           _inputEvent.Invoke(_inputData);   
        }
    }
}