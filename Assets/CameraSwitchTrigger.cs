using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _barrier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameEvents.instance.EnterSwitchCamera();

        if (_barrier != null)
            _barrier.SetActive(true);

        gameObject.SetActive(false);
    }
}
