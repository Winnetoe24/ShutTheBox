 using System;
 using System.Collections.Generic;
 using System.IO;
 using System.Linq;
 using System.Net.Http;
 using System.Threading;
 using System.Threading.Tasks;
class Programm
{
    private const String trenner = "--------------------------------------";
    private static int[] zahlen = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
    static bool wurfeln = true;

    static void Main(string[] args)
    {
        Console.Write("-");
        bool running = true;
        int gewuerfelt = 1;
        printHelp();

        while (running)
        {
            //Würfeln
            if (wurfeln)
            {
                gewuerfelt = (new Random().Next() % 12)+1;
                wurfeln = false;
            }
 
            //Print
            Console.WriteLine(trenner);
            for (int i = 1; i < zahlen.Length+1; i++)
            {
                System.Console.Write("{0} ", i);
            }
            Console.WriteLine(" ");
            foreach (int i in zahlen)
            {
                if (i == -1)
                {
                    System.Console.Write("X ");
                }
                else
                {
                    System.Console.Write("{0} ", i);
 
                }
            }

            Console.WriteLine(" ");
            Console.WriteLine("Gewürfelte Zahl: {0}", gewuerfelt);
            
            //Analyse
            int auswahlGesammt = 0;
            for (int index = 0; index < zahlen.Length; index++)
            {
                int i = zahlen[index];
                if (i == 0)
                {
                    auswahlGesammt += index+1;
                }
            }

            if (auswahlGesammt > gewuerfelt)
            {
                Console.WriteLine("Auswahl größer als gewürfelte Zahl!");
            }
            
            //Handle User Input
            string? readLine = Console.ReadLine();
            if (readLine == null) {
             Console.WriteLine("null");
                continue;
            }
            switch (readLine)
            {
                case "help" :
                    printHelp();
                    continue;
                case "X" :
                    handleCommit(auswahlGesammt, gewuerfelt);
                    continue;
                case "stop" :
                    running = false;
                    continue;
                case "": continue;
                case "reset" :
                    zahlen = new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
                    wurfeln = true;
                    continue;
                    
            }
            var strings = readLine.Split(" ");
           

            for (var i = 0; i < strings.Length; i++)
            {
                bool cancel = false;
                switch (strings[i])
                {
                    case "X":
                        handleCommit(auswahlGesammt, gewuerfelt);
                        cancel = true;
                        break;
                    default:
                        int auswahl = Convert.ToInt32(strings[i]);
                        if (auswahl <= 0 || auswahl > 12)
                        {
                            Console.WriteLine("{0} ist keine gültige Zahl", auswahl);
                            continue;
                        }
                        auswahl--;
                        int zahl = zahlen[auswahl];
                        if (zahl != -1)
                        {
                            if (zahl != 0)
                            {
                                auswahlGesammt += zahl;
                                zahlen[auswahl] = 0;
                            }
                            else
                            {
                                zahlen[auswahl] = auswahl+1;
                            }
                        }
                        break;
                }

                if (cancel)
                {
                    break;
                }
                

            }
            
        }
    }

    static void printHelp()
    {
        Console.WriteLine(trenner);
        Console.WriteLine("Shut the Box");
        Console.WriteLine(trenner);
        
        Console.WriteLine("Ziel des Spiels:");
        Console.WriteLine(" Decke alle Zahlen mit den gewürfelten Zahlen ab");
        Console.WriteLine("Ablauf:");
        Console.WriteLine(" Jede Runde wird eine Zahl gewürfelt");
        Console.WriteLine(" Diese kann dann beliebig in Zahlen aufgeteilt werden.");
        Console.WriteLine(" Die aufgeteilten Zahlen muss der Spieler eingeben");
        Console.WriteLine(" Eine Eingabe wird mithilfe eines 'X' bestätigt");
        Console.WriteLine("Anzeige:");
        Console.WriteLine(" ausgewählte Zahlen werden mit einem 0 markiert");
        Console.WriteLine(" verwendete Zahlen werden mit einem X markiert");
        Console.WriteLine("Commands:");
        Console.WriteLine(" X - Auswahl bestätigen");
        Console.WriteLine(" stop - beendet das Programm");;
        Console.WriteLine(" reset - setzt das Programm zurück");;
        Console.WriteLine(" help - zeigt diese Hilfe");
        
        
    }

    static void handleCommit(int auswahlGesammt, int gewuerfelt)
    {
        if (auswahlGesammt > gewuerfelt)
        {
            Console.WriteLine("Auswahl größer als gewürfelte Zahl");
            return;
        }

        if (auswahlGesammt != gewuerfelt)
        {
            Console.WriteLine("Auswahl ergibt nicht die gewürfelt Zahl");
            return;
        }
        for (int i = 0; i < zahlen.Length; i++)
        {
            int x = zahlen[i];
            if (x == 0)
            {
                zahlen[i] = -1;
            }
        }
        wurfeln = true;
    }
}