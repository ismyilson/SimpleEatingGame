using UnityEngine;

public class Enemy : Point
{
    public override void Start()
    {
        base.Start();

        setSize(Random.Range(GameManager.Instance.GetMinSize(), GameManager.Instance.GetMaxSize()));
        setColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
    }

    public void Move(Vector2 direction)
    {
        Vector2 velocity = new Vector2();
        if (direction.x > transform.position.x)
            velocity.x = GameManager.Instance.GetEnemySpeed();
        else
            velocity.x = -GameManager.Instance.GetEnemySpeed();

        if (direction.y > transform.position.y)
            velocity.y = GameManager.Instance.GetEnemySpeed();
        else
            velocity.y = -GameManager.Instance.GetEnemySpeed();

        _body.velocity = velocity;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
