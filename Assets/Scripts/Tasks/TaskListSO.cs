using UnityEngine;

[CreateAssetMenu(fileName = "New task list", menuName = "Tasks/TaskList")]
public class TaskListSO : ScriptableObject
{
    public string Title;
    public TaskSO[] Tasks;
}

