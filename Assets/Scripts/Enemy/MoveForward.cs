using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed = 5.00f;

    private void Start()
    {
        AudioManager.instance.Play("Snowball");
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

}
