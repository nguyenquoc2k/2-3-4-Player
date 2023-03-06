using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AIStickBallMap1 : MonoBehaviour
{
    public int indexPlayer;
    [SerializeField] float speed;
    [SerializeField] Vector2 limitX;
    [SerializeField] Transform characterHolder;

    [SerializeField] bool left;
    [SerializeField] bool right;

    [SerializeField] bool up;

    //[SerializeField] ParticleSystem deadEffect;
    //[SerializeField] Transform contactEffect;
    bool isContactEffect = false;

    int direction = 1;

    [SerializeField] Vector2 ballPos;

    void Start()
    {
        StartCoroutine(ChoiceNearBall());
    }

    void FixedUpdate()
    {
        if (ballPos == Vector2.zero) return;

        if (left)
        {
            if (ballPos.y > characterHolder.position.y + 0.4f)
            {
                characterHolder.localPosition =
                    new Vector2(characterHolder.localPosition.x - speed * Time.fixedDeltaTime, 0);
            }
            else if (ballPos.y < characterHolder.position.y - 0.4f)
            {
                characterHolder.localPosition =
                    new Vector2(characterHolder.localPosition.x + speed * Time.fixedDeltaTime, 0);
            }
        }
        else if (right)
        {
            if (ballPos.y > characterHolder.position.y + 0.4f)
            {
                characterHolder.localPosition =
                    new Vector2(characterHolder.localPosition.x + speed * Time.fixedDeltaTime, 0);
            }
            else if (ballPos.y < characterHolder.position.y - 0.4f)
            {
                characterHolder.localPosition =
                    new Vector2(characterHolder.localPosition.x - speed * Time.fixedDeltaTime, 0);
            }
        }
        else if (up)
        {
            if (ballPos.x > characterHolder.position.x + 0.4f)
            {
                characterHolder.localPosition =
                    new Vector2(characterHolder.localPosition.x - speed * Time.fixedDeltaTime, 0);
            }
            else if (ballPos.x < characterHolder.position.x - 0.4f)
            {
                characterHolder.localPosition =
                    new Vector2(characterHolder.localPosition.x + speed * Time.fixedDeltaTime, 0);
            }
        }


        characterHolder.localPosition = new Vector2(Mathf.Clamp(characterHolder.localPosition.x, limitX.x, limitX.y),
            characterHolder.localPosition.y);
    }

    public void Dead()
    {
        //deadEffect.transform.position = characterHolder.position;
        //deadEffect.Play();
        ShakeCamera(Camera.main.transform, 1, 0.13f, 47, 83, DG.Tweening.Ease.OutQuad);
        transform.parent.gameObject.SetActive(false);
    }


    IEnumerator ChoiceNearBall()
    {
        yield return new WaitForSeconds(Random.Range(0.15f, 0.3f));

        while (true)
        {
            if (CannonStickBall.instance != null && CannonStickBall.instance.GetBulletNumber() > 0)
            {
                float distanceToBall = 10f;
                Transform ball = null;


                foreach (var ballActive in CannonStickBall.instance.bulletHolder.GetComponentsInChildren<Transform>())
                {
                    if (ball != null)
                    {
                        if (left)
                        {
                            if (ballActive.transform.position.x > characterHolder.transform.position.x)
                            {
                                float currentDistance = Mathf.Abs(ballActive.transform.position.x -
                                                                  characterHolder.transform.position.x);
                                if (currentDistance < distanceToBall)
                                {
                                    distanceToBall = currentDistance;
                                    ball = ballActive.transform;
                                    ballPos = ball.position;
                                }

                                if (distanceToBall < 2)
                                {
                                    break;
                                }
                            }
                        }
                        else if (right)
                        {
                            if (ballActive.transform.position.x < characterHolder.transform.position.x)
                            {
                                float currentDistance = Mathf.Abs(ballActive.transform.position.x -
                                                                  characterHolder.transform.position.x);
                                if (currentDistance < distanceToBall)
                                {
                                    distanceToBall = currentDistance;
                                    ball = ballActive.transform;
                                    ballPos = ball.position;
                                }

                                if (distanceToBall < 2)
                                {
                                    break;
                                }
                            }
                        }
                        else if (up)
                        {
                            if (ballActive.transform.position.y < characterHolder.transform.position.y)
                            {
                                float currentDistance = Mathf.Abs(ballActive.transform.position.y -
                                                                  characterHolder.transform.position.y);
                                if (currentDistance < distanceToBall)
                                {
                                    distanceToBall = currentDistance;
                                    ball = ballActive.transform;
                                    ballPos = ball.position;
                                }

                                if (distanceToBall < 2)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        ball = ballActive.transform;
                    }
                }
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator ContactEffect(Vector2 pos)
    {
        isContactEffect = true;
        //contactEffect.transform.position = pos;
        //contactEffect.gameObject.SetActive(true);

        ShakeCamera(Camera.main.transform, 1, 0.13f, 47, 83, DG.Tweening.Ease.OutQuad);

        yield return new WaitForSeconds(0.1f);

        // contactEffect.gameObject.SetActive(false);
        isContactEffect = false;
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

    void ShakeCamera(Transform mTransform, float strength, float duration, int vibrato, float randomNess, Ease eas)
    {
        mTransform.DOShakePosition(duration, strength, vibrato, randomNess).SetEase(eas);
    }
}