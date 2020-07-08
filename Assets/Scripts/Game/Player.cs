using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Point
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        setSize(GameManager.Instance.GetMinSize() + 0.1f);
        setColor(Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state != GameState.STATE_PLAYING)
            return;

        MoveToMouse();
    }

    void Grow()
    {
        setSize(getSize() + 0.03f);

        GameManager.Instance.SetMinSize(getSize() - 0.2f);
        GameManager.Instance.SetMaxSize(getSize() + 0.5f);
    }

    void MoveToMouse()
    {
        var mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = -1;

        transform.position = mousePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.state != GameState.STATE_PLAYING)
            return;

        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (!enemy)
            return;

        if (getSize() >= enemy.getSize())
        {
            GameManager.Instance.IncreaseScore();
            Grow();
            Destroy(enemy.gameObject);
        }
        else
        {
            GameManager.Instance.FinishGame();
        }
    }
}
