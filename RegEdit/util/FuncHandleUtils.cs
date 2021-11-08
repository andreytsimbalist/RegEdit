using System;

namespace RegEdit.util
{
    public static class FuncHandleUtils
    {
        public static TResult Execute<TResult>(Func<TResult> func, TResult defaultResult)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return defaultResult;
            }
        }
    }
}