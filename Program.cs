using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        double lowNumber = GetValidNumber("Input a positive low number: ", positiveOnly: true);
        double highNumber = GetValidNumber("Input a high number greater than the low number: ", lowNumber);

        List<double> numbers = Enumerable.Range((int)lowNumber, (int)(highNumber - lowNumber + 1)).Select(x => (double)x).ToList();
        WriteNumbersToFile(numbers, "numbers.txt");

        double sum = ReadNumbersFromFileAndSum("numbers.txt");
        Console.WriteLine($"The sum of the numbers is: {sum}");

        Console.WriteLine("Prime numbers between low and high:");
        foreach (var number in numbers)
        {
            if (IsPrime(number))
            {
                Console.WriteLine(number);
            }
        }
    }

    static double GetValidNumber(string prompt, double minimum = 0, bool positiveOnly = false)
    {
        double validNumber;
        do
        {
            Console.Write(prompt);
            if (!double.TryParse(Console.ReadLine(), out validNumber) || (positiveOnly && validNumber <= 0) || validNumber <= minimum)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
        while ((positiveOnly && validNumber <= 0) || validNumber <= minimum);

        return validNumber;
    }

    static void WriteNumbersToFile(List<double> numbers, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (double number in numbers.OrderByDescending(x => x))
            {
                writer.WriteLine(number);
            }
        }
    }

    static double ReadNumbersFromFileAndSum(string filePath)
    {
        double sum = 0;
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            if (double.TryParse(line, out double number))
            {
                sum += number;
            }
        }
        return sum;
    }

    static bool IsPrime(double number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
        {
            if (number % i == 0) return false;
        }

        return true;
    }
}
