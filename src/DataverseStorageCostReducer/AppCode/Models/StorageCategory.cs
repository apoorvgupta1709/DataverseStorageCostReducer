namespace DataverseStorageCostReducer.AppCode.Models
{
    public enum StorageCategory
    {
        Database,
        File,
        Log
    }

    public enum AttachmentSource
    {
        EmailAttachment,
        NoteAnnotation
    }

    public enum AsyncJobState
    {
        Ready = 0,
        Suspended = 1,
        Locked = 2,
        Completed = 3
    }

    public enum AsyncJobStatusCode
    {
        WaitingForResources = 0,
        Waiting = 10,
        InProgress = 20,
        Pausing = 21,
        Canceling = 22,
        Succeeded = 30,
        Failed = 31,
        Canceled = 32
    }
}
