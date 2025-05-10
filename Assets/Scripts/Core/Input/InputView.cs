using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Core.Input
{
    public class InputView : MonoBehaviour, IPointerDownHandler
    {
        [Inject] private InputModel _inputModel;

        public void OnPointerDown(PointerEventData eventData)
        {
            _inputModel.OnPointerDown?.Invoke(eventData);
        }
    }
}