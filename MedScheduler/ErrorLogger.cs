using System;
using System.IO;

public static class ErrorLogger
{
    public static void LogError(Exception ex)
    {
        try
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorLog.txt");
            string logMessage = $"Error occurred at {DateTime.Now}{Environment.NewLine}Message: {ex.Message}{Environment.NewLine}Stack Trace: {ex.StackTrace}";
            File.AppendAllText(logFilePath, logMessage);
        }
        catch (Exception logEx)
        {
            // Handle or log any errors that occur during the logging process
            // For troubleshooting purposes, you can use Console.WriteLine or a logger to output the error log exception
            Console.WriteLine("Error occurred during logging: " + logEx.Message);
        }
    }
}
