using UnityEngine;

public class RestartRunning : StateMachineBehaviour
{
    static int s_DeadHash = Animator.StringToHash("Dead");

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // We don't restart if we go toward the death state
        if (animator.GetBool(s_DeadHash))
            return;

        // Check if TrackManager.instance is not null before calling StartMove
        if (TrackManager.instance != null)
        {
            TrackManager.instance.StartMove();
        }
        else
        {
            Debug.LogError("TrackManager.instance is null. Make sure it's properly initialized.");
        }
    }
}