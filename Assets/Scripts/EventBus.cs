using UnityEngine;

public class EventBus
{
    public static Signal<int> PlayerWantToStart = new Signal<int>();

    public static Signal StartGame = new Signal();
    public static Signal FinishLevel = new Signal();
    public static Signal<Vector3, float> TheHandIsDown = new Signal<Vector3, float>();
    public static Signal VirginDied = new Signal();
    public static Signal DemonAngry = new Signal();
    public static Signal EndGame = new Signal();
}
