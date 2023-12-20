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
                // 플레이어와 충돌한 경우 게임 멈추기
                StopGame();
            }
        }

        void StopGame()
        {
            // 게임을 멈추는 코드
            Time.timeScale = 0f;

            // 게임오버 UI를 표시하거나 추가적인 처리를 할 수 있습니다.
            // 예: gameOverUI.SetActive(true);
        }
    }
}

