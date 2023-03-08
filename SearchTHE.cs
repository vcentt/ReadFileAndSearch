using System.IO;
using System.Text.RegularExpressions;

class Solutions 
{
    public void CodePure()
    {
        string fileContent = File.ReadAllText("./exam.txt");
        fileContent = fileContent.ToLower();
        int cnt = 0;

        long startMemory = GC.GetTotalMemory(true);
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        GC.WaitForPendingFinalizers();
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        DateTime sT = DateTime.Now;
        
        for(int i=0; i<fileContent.Length; i++)
        {
            if (fileContent[i] == 't' && fileContent[i + 1] == 'h' && fileContent[i + 2] == 'e')
            {
                cnt++;
            }
        }
        
        DateTime eT = DateTime.Now;
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Default);
        long endMemory = GC.GetTotalMemory(true);

        long usedMemory = endMemory - startMemory;
        double totalTimeExecution = (eT-sT).TotalMilliseconds;
        
        Console.WriteLine("PATTERN A - SOLUTION PURE CODE");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine($"Numbers of words: {cnt}");
        Console.WriteLine($"Execution time: {totalTimeExecution} ms");
        Console.WriteLine($"Memory used during execution: {usedMemory / 1024} KB");
        Console.WriteLine("-------------------------------------");
    }

    public void DfaMethod()
    {
        var fileContent = File.ReadAllText("./exam.txt");
        fileContent = fileContent.ToLower();
        int counter = 0;
        string currentState = "q0";

        long startMemory = GC.GetTotalMemory(true);
        
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        GC.WaitForPendingFinalizers();
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        DateTime sT = DateTime.Now;

        for (int cnt = 0; cnt < fileContent.Length; cnt += 1)
        {
            switch (currentState)
            {
              case "q0":
                  if (fileContent[cnt] == 't')
                  {
                      currentState = "q1";
                  }
                  break;
              case "q1":
                  if (fileContent[cnt] == 'h')
                  {
                      currentState = "q2";
                  }
                  else if (fileContent[cnt] == 't')
                  {
                      currentState = "q1";
                  }
                  else
                  {
                      currentState = "q0";
                  }
                  break;
              case "q2":
                  if (fileContent[cnt] == 'e')
                  {
                      currentState = "finalState";
                  }
                  else if (fileContent[cnt] == 't')
                  {
                      currentState = "q1";
                  }
                  else
                  {
                      currentState = "q0";
                  }
                  break;
              case "finalState":
                  counter += 1;
                  currentState = "q0";
                  break;
            }
        }

        DateTime eT = DateTime.Now;
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Default);
        long endMemory = GC.GetTotalMemory(true);
        
        long usedMemory = endMemory - startMemory;
        double totalTimeExecution = (eT-sT).TotalMilliseconds;
        
        Console.WriteLine("PATTERN A - SOLUTION DFA CODE");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine($"Numbers of words: {counter}");
        Console.WriteLine($"Execution time: {totalTimeExecution} ms");
        Console.WriteLine($"Memory used during execution: {usedMemory / 1024} KB");
        Console.WriteLine("-------------------------------------");
    }
    
    public void CodeRegex()
    {
        string fileContent = File.ReadAllText("./exam.txt");
        fileContent = fileContent.ToLower();

        long startMemory = GC.GetTotalMemory(true);
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        GC.WaitForPendingFinalizers();
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        
        string pattern = @"the";
        Regex regex = new Regex(pattern);
        DateTime sT = DateTime.Now;
        MatchCollection matches = regex.Matches(fileContent);
        DateTime eT = DateTime.Now;
        int count = matches.Count;
        
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Default);
        long endMemory = GC.GetTotalMemory(true);

        long usedMemory = endMemory - startMemory;
        double totalTimeExecution = (eT-sT).TotalMilliseconds;

        Console.WriteLine("PATTERN A - SOLUTION REGEX");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine($"Numbers of words: {count}");
        Console.WriteLine($"Execution time: {totalTimeExecution} ms");
        Console.WriteLine($"Memory used during execution: {usedMemory / 1024} KB");
        Console.WriteLine("-------------------------------------");
    }
}

class RunCode 
{
    static void Main(string[] args)
    {
        Solutions viewSolutions = new Solutions();
        viewSolutions.CodePure();
        Console.WriteLine();
        Console.WriteLine();
        viewSolutions.DfaMethod();
        Console.WriteLine();
        Console.WriteLine();
        viewSolutions.CodeRegex();
        
    }
}