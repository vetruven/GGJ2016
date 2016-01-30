using UnityEngine;

public class EventBus
{
    public static Signal<int> PlayerWantToStart = new Signal<int>();

    public static Signal StartGame = new Signal();
    public static Signal StartLevel = new Signal();
    public static Signal RestartGame = new Signal();
    public static Signal FinishLevel = new Signal();
    public static Signal<Vector3, float> TheHandIsDown = new Signal<Vector3, float>();
    public static Signal VirginDied = new Signal();
    public static Signal<int> TotalVirginsDied = new Signal<int>();
    public static Signal HandHasGrabbed = new Signal();
    public static Signal DemonAngry = new Signal();
    public static Signal UpdateBar = new Signal();
    public static Signal BarEmpty = new Signal();
    public static Signal EndGame = new Signal();
    public static Signal BeaconActivated = new Signal();
    public static Signal BeaconDeactivated = new Signal();
    public static Signal GrapplerActivated = new Signal();
    public static Signal GrapplerDeactivated = new Signal();
    public static Signal IllusionActivated = new Signal();
    public static Signal IllusionDeactivated = new Signal();
    public static Signal SprintActivated = new Signal();
}
