using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AudioManager audioManager;
    public bool onTarget;
    public Sprite onTargetSprite;
    public Sprite normalSprite;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool Move(Vector2 direction)
    {
        if (BoxBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            TransformOnTarget();
            audioManager.Play("boxSound");
            return true;
        }
    }

    private void TransformOnTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
        {
            if (transform.position.x == target.transform.position.x && transform.position.y == target.transform.position.y + 0.5)
            {
                spriteRenderer.sprite = onTargetSprite;
                onTarget = true;
                return;
            }
        }
        spriteRenderer.sprite = normalSprite;
        onTarget = false;
    }

    private bool BoxBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        return false;
    }
}
