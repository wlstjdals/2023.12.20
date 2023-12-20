using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // �÷��̾�� �浹�� ��� ���� ���߱�
                StopGame();
            }
        }

        void StopGame()
        {
            // ������ ���ߴ� �ڵ�
            Time.timeScale = 0f;

            // ���ӿ��� UI�� ǥ���ϰų� �߰����� ó���� �� �� �ֽ��ϴ�.
            // ��: gameOverUI.SetActive(true);
        }
    }
}

