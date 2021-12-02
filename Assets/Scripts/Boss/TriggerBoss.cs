using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public bool isTriggered = false;
    private GameObject _boss;
    private Animator _bossAnimator;
    private GameObject _passageBlock;
    private SpriteRenderer _blockSpriteRenderer;
    private Vector3 _bossStartingPosition;
    private BossHealth _bossHealth;

    private void Start()
    {
        _boss = GameObject.FindGameObjectWithTag("Boss");
        _bossAnimator = _boss.GetComponent<Animator>();
        _bossStartingPosition = _boss.transform.position;
        _bossHealth = _boss.GetComponent<BossHealth>();

        _passageBlock = GameObject.Find("BossPassageBlock");
        _blockSpriteRenderer = _passageBlock.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered)
            return;

        _bossAnimator.SetBool("isBossTriggered", true);
       
        isTriggered = true;

        _blockSpriteRenderer = _passageBlock.GetComponent<SpriteRenderer>();

        Color color = _blockSpriteRenderer.color;
        color.a = 1.0f;
        _blockSpriteRenderer.color = color;

        AudioManager.instance.Play("BossTrigger");

        _passageBlock.GetComponent<BoxCollider2D>().enabled = true;

    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void UndoTrigger()
    {
        _bossAnimator.SetBool("isBossTriggered", false);
        _boss.transform.position = _bossStartingPosition;

        _bossHealth.health = _bossHealth.maxHealth;

        isTriggered = false;

        Color color = _blockSpriteRenderer.color;
        color.a = 0.2f;
        _blockSpriteRenderer.color = color;

        _passageBlock.GetComponent<BoxCollider2D>().enabled = false;

        GetComponent<BoxCollider2D>().enabled = true;

    }
}
