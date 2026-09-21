namespace Delex_POS.Application.Common.Extensions;

public static class SyncExtension
{
    private static readonly TaskFactory MyTaskFactory = new(CancellationToken.None, 
            TaskCreationOptions.None, 
            TaskContinuationOptions.None, 
            TaskScheduler.Default);

    public static TResult RunSync<TResult>(Func<Task<TResult>> func)
    {
        return MyTaskFactory
            .StartNew<Task<TResult>>(func)
            .Unwrap<TResult>()
            .GetAwaiter()
            .GetResult();
    }

    public static void RunSync(Func<Task> func)
    {
        MyTaskFactory
            .StartNew<Task>(func)
            .Unwrap()
            .GetAwaiter()
            .GetResult();
    }
}
