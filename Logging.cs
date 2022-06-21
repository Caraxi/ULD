namespace ULD;

public static class Logging {
    public delegate void LogAction(string message, params object[] parameters);
    
    public static event LogAction LogMessage;
    
    internal static void Log(string message) {
        var indent = new string('\t', indentCount);
        LogMessage?.Invoke($"{indent}{message}");
    }

    internal static void Indent() {
        indentCount++;
    }
    
    internal static void Unindent() {
        indentCount--;
        if (indentCount < 0) indentCount = 0;
    }
    
    internal static void ZeroIndent() {
        indentCount = 0;
    }

    internal static void IndentLog(string message) {
        Log(message);
        Indent();
    }
    
    
    

    private static int indentCount = 0;

}
