public interface IStateEnumerator<T>
    where T : System.Enum
{
    public enum Step
    {
        None,
        Start,
        Casting,
        DoAction,
        WaitUntilDoActionFinished,
        Finish,
    }

    Step current { get; set; }

    bool canExcute { get; set; }

    T MoveNext();

    void Reset();
}
