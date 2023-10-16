//using UnityEngine;

//public class SearchingForBricksState : BotState
//{
//    public SearchingForBricksState(BotPlayer bot) : base(bot) { }

//    public override void Tick()
//    {
//        // Implement the behavior of the bot when it's searching for bricks
//        GameObject nearestBrick = FindNearestBrick();
//        if (nearestBrick != null)
//        {
//            bot.agent.SetDestination(nearestBrick.transform.position);
//        }
//    }

//    private GameObject FindNearestBrick()
//    {
//        GameObject[] bricksInScene = GameObject.FindGameObjectsWithTag("Brick");
//        GameObject nearestBrick = null;
//        float minDistance = Mathf.Infinity;
//        foreach (GameObject brick in bricksInScene)
//        {
//            float distance = Vector3.Distance(bot.transform.position, brick.transform.position);
//            if (distance < minDistance)
//            {
//                minDistance = distance;
//                nearestBrick = brick;
//            }
//        }
//        return nearestBrick;
//    }
//}
