using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int lowNumber = GetValidNumber("Enter the low number: ", false);
        int highNumber = GetValidNumber("Enter the high number: ", true, lowNumber);

        Console.WriteLine($"Difference: {highNumber - lowNumber}");

        List<double> numbers = new List<double>();
        for (double i = lowNumber; i <= highNumber; i++)
        {
            numbers.Add(i);
        }

        string filePath = "numbers.txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                writer.WriteLine(numbers[i]);
            }
        }

        double sum = 0;
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                sum += double.Parse(line);
            }
        }

        Console.WriteLine($"Sum of numbers: {sum}");

        Console.WriteLine("Prime numbers:");
        foreach (double number in numbers)
        {
            if (IsPrime(number))
            {
                Console.WriteLine(number);
            }
        }
    }

    static int GetValidNumber(string message, bool checkGreater, int lowNumber = 0)
    {
        int number;
        do
        {
            Console.Write(message);
        } while (!int.TryParse(Console.ReadLine(), out number) || (checkGreater && number <= lowNumber));

        return number;
    }

    static bool IsPrime(double number)
    {
        if (number < 2) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }
}