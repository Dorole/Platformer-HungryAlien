using UnityEngine;

public class BossSprites : MonoBehaviour
{
    [SerializeField] private BossHealth _bossHealth;

    [SerializeField] private Sprite _healthySprite;
    [SerializeField] private Sprite _damagedSprite;
    [SerializeField] private Sprite _destroyedSprite;

    [SerializeField] private Sprite _currentSprite;

    private void Start()
    {
        _currentSprite = GetComponent<SpriteRenderer>().sprite;
    }

    //animation override controller??
    private void Update()
    {
        if (_bossHealth.health >= 60 && _currentSprite != _healthySprite)
            ChangeSprite(_healthySprite);

        if (_bossHealth.health < 60 && _bossHealth.health >= 30 && _currentSprite != _damagedSprite)
            ChangeSprite(_damagedSprite);

        if (_bossHealth.health < 30 && _currentSprite != _destroyedSprite)
            ChangeSprite(_destroyedSprite);
    }

    private void ChangeSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        _currentSprite = sprite;
    }


}
