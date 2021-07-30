using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public Rigidbody2D PlayerRigidbody;
    //readonly float MovementSpeed = 2.5f;
    private bool readyForInput = false;
    public Animator PlayerAnimator;
    public SpriteRenderer PlayerSpriteRenderer;
    private Vector2 movement;
    public GameObject WinPanel;
    public GameObject LevelTextPanel;
    public GameObject PauseMenuPanal;

    private void Start()
    {
        StartCoroutine(WaitForLevelText());
    }

    IEnumerator WaitForLevelText()
    {
        LevelTextPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LevelTextPanel.SetActive(false);
    }

    void Update()
    {
        if(!LevelTextPanel.activeSelf && !WinPanel.activeSelf && !PauseMenuPanal.activeSelf)
        {
            MovePlayer();
        }else if(PauseMenuPanal.activeSelf && Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenuPanal.SetActive(false);
        }
    }

    private void MovePlayer()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenuPanal.SetActive(true);
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        PlayerAnimator.SetFloat("Horizontal", movement.x);
        PlayerAnimator.SetFloat("Vertical", movement.y);
        PlayerAnimator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x > 0)
        {
            PlayerSpriteRenderer.flipX = true;
        }
        else
        {
            PlayerSpriteRenderer.flipX = false;
        }

        Vector2 input = new Vector2(movement.x, movement.y);
        input.Normalize();

        if (input.sqrMagnitude > 0.5)
        {
            if (readyForInput)
            {
                readyForInput = false;
                Move(input);
                WinPanel.SetActive(IsLevelComplete());
            }
        }
        else
        {
            readyForInput = true;
        }
    }

    //private void FixedUpdate()
    //{
    //    PlayerRigidbody.MovePosition(PlayerRigidbody.position + movement * MovementSpeed * Time.fixedDeltaTime);
    //}

    public bool Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.5)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize();
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            return true;
        }
    }

    bool Blocked(Vector3 position, Vector2 direction)
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
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsLevelComplete()
    {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach(Box box in boxes)
        {
            if (!box.onTarget) return false;
        }
        return true;
    }
}
