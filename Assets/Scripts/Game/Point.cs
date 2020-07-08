using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class Point : MonoBehaviour
{
    protected SpriteRenderer _sprite;
    protected Collider2D _collider;
    protected Rigidbody2D _body;

    private float _size = 1f;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _body = GetComponent<Rigidbody2D>();
    }

    public virtual void Start()
    {

    }
    
    protected void setColor(Color color)
    {
        _sprite.color = color;
    }

    protected void setSize(float size)
    {
        if (size < 0.05f)
            return;

        _size = size;
        gameObject.transform.localScale = new Vector3(_size, _size, _size);
    }

    public float getSize()
    {
        return _size;
    }
}
