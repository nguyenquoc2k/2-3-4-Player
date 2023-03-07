using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerStickBall : MonoBehaviour
{
    public int indexPlayer;
    float speed = 10;
    [SerializeField] Vector2 limitX;

    int direction = 1;

    [SerializeField] Transform characterHolder;
    //[SerializeField] TapButton changerDirectionButton;

    //[SerializeField] Transform contactEffect;
    bool isContactEffect = false;
    //[SerializeField] ParticleSystem deadEffect;

    bool isMove = false;

    void Awake()
    {
        limitX = new Vector2(-2.35f, 2.35f);
        characterHolder = transform;
        if (transform.parent.name == "Player1")
        {
            ReverseGravityController.ReversePlayer1 += ChangerDirection;
            indexPlayer = 0;
        }
        else if (transform.parent.name == "Player2")
        {
            ReverseGravityController.ReversePlayer2 += ChangerDirection;
            indexPlayer = 1;
        }
        else if (transform.parent.name == "Player3")
        {
            ReverseGravityController.ReversePlayer3 += ChangerDirection;
            indexPlayer = 2;
        }
        else if (transform.parent.name == "Player4")
        {
            ReverseGravityController.ReversePlayer4 += ChangerDirection;
            indexPlayer = 3;
        }
        StartCoroutine(ActivePlayer());
    }

    private void OnDestroy()
    {
        if (transform.parent.name == "Player1") ReverseGravityController.ReversePlayer1 -= ChangerDirection;
        else if (transform.parent.name == "Player2") ReverseGravityController.ReversePlayer2 -= ChangerDirection;
        else if (transform.parent.name == "Player3") ReverseGravityController.ReversePlayer3 -= ChangerDirection;
        else if (transform.parent.name == "Player4") ReverseGravityController.ReversePlayer4 -= ChangerDirection;
    }

    IEnumerator ActivePlayer()
    {
        yield return new WaitForSeconds(3);
        //changerDirectionButton.gameObject.SetActive(true);
        isMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isMove) return;
        characterHolder.Translate(new Vector2(speed * direction * Time.fixedDeltaTime, 0));
        characterHolder.localPosition = new Vector2(Mathf.Clamp(characterHolder.localPosition.x, limitX.x, limitX.y),
            characterHolder.localPosition.y);
    }

    void ChangerDirection(bool buttonState)
    {
        direction *= -1;
    }


    public void Dead()
    {
        //deadEffect.transform.position = characterHolder.position;
        // deadEffect.Play();
        ShakeCamera(Camera.main.transform, 1, 0.13f, 47, 83, DG.Tweening.Ease.OutQuad);
        //changerDirectionButton.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            if (!isContactEffect)
            {
                StartCoroutine(ContactEffect(collision.contacts[0].point));
            }
        }
    }

    public static void ShakeCamera(Transform mTransform, float strength, float duration, int vibrato, float randomNess,
        Ease eas)
    {
        mTransform.DOShakePosition(duration, strength, vibrato, randomNess).SetEase(eas);
    }

    IEnumerator ContactEffect(Vector2 pos)
    {
        isContactEffect = true;
        //contactEffect.transform.position = pos;
        //contactEffect.gameObject.SetActive(true);

        ShakeCamera(Camera.main.transform, 1, 0.13f, 47, 83, DG.Tweening.Ease.OutQuad);

        yield return new WaitForSeconds(0.1f);

        //contactEffect.gameObject.SetActive(false);
        isContactEffect = false;
    }
}