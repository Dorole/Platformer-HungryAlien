using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class RuneLock : MonoBehaviour
{
	public KeyColor runeToUnlock = KeyColor.Blue;

	private SpriteRenderer _spriteRenderer;
	private BoxCollider2D _boxCollider2D;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_boxCollider2D = GetComponent<BoxCollider2D>();

		RuneKey.OnRuneCollected.AddListener(Unlock);

	}

    private void Unlock(KeyColor collectedRune)
	{
		if (collectedRune != runeToUnlock)
			return;

		Color color = _spriteRenderer.color;
		color.a = 0.5f;
		_spriteRenderer.color = color;

		_boxCollider2D.enabled = false;
	}
}
