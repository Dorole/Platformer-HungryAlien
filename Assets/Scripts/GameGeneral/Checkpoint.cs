using Cinemachine;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        
        if (_animator != null)
            _animator.SetBool("IsVictory", true);

        AudioManager.instance.Play("Checkpoint");
        GameManager.instance.spawnPoint = gameObject;
        GameManager.instance.virtualCamera = cam; 

        gameObject.GetComponent<Collider2D>().enabled = false;

    }

}
