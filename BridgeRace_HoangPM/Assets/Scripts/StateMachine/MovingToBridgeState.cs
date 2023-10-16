//using UnityEngine;

//public class MovingToBridgeState : BotState
//{
//    public MovingToBridgeState(BotPlayer bot) : base(bot) { }

//    public override void Tick()
//    {
//        // Implement the behavior of the bot when it's moving to the bridge
//        GameObject nearestBridge = FindNearestBridge();
//        if (nearestBridge != null)
//        {
//            bot.agent.SetDestination(nearestBridge.transform.position);
//        }
//    }

//    private GameObject FindNearestBridge()
//    {
//        GameObject[] bridgesInScene = GameObject.FindGameObjectsWithTag("BridgeTile");
//        GameObject nearestBridge = null;
//        float minDistance = Mathf.Infinity;
//        foreach (GameObject bridge in bridgesInScene)
//        {
//            float distance = Vector3.Distance(bot.transform.position, bridge.transform.position);
//            if (distance < minDistance)
//            {
//                minDistance = distance;
//                nearestBridge = bridge;
//            }
//        }
//        return nearestBridge;
//    }
//}
