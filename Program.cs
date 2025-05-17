using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //File path to the data file to be used for this project
            string filePath = @"C:\Users\thwal\Desktop\Portfolio\Sort and Search Tool\AssignementData.txt";

            //Declarations for the Searching type, Sorting type and valid input which checks if user input is correct
            int searchType = 0, sortType = 0;

            //Reading from the data file
            StreamReader myReader = new StreamReader(filePath);

            //Instatiating a List class
            List<string> lines = new List<string>();

            string currentRow = myReader.ReadLine();

            //While loop to read from the data file and add the information from the file into the list
            while (!myReader.EndOfStream)
            {
                lines.Add(currentRow);

                currentRow = myReader.ReadLine();
            }

            //Reading the last line in the list
            lines.Add(currentRow);

            //Closing the StreamReader
            myReader.Close();

            //Calling the method to choose the seaching type
            searchType = ChooseSearchType(searchType);

            //making a new blank line
            Console.WriteLine("\n");

            //If statement with code for linear search for if the user selects option 1
            if (searchType == 1)
            {
                int numberToFind = GetNumber("Which number are you searching for? ");
                LinearSearch(numberToFind, filePath);

                //Displaying the list
                DisplayList(lines);
            }

            //If statement with code for binary search for if the user selects option 2
            else if (searchType == 2)
            {
                //Calling the method to choose the sorting type
                sortType = ChooseSortType(sortType);

                //making a new blank line
                Console.WriteLine("\n");

                //If statement with code for bubble sort for if the user selects option 1
                if (sortType == 1)
                {
                    BubbleSort(lines);
                    Console.WriteLine("The list was sorted using the bubble sort algorithm");

                    //making a new blank line
                    Console.WriteLine("\n");

                    int numberToFind = GetNumber("Which number are you searching for? ");
                    int indexToFind = BinarySearch(lines, numberToFind);

                    if (indexToFind == -1)
                    {
                        Console.WriteLine("The number {0} was not found", numberToFind);
                    }
                    else
                    {
                        Console.WriteLine("The number {0} was found at line {1} in the list", numberToFind, indexToFind);
                    }

                    Console.WriteLine("\n");

                    //Displaying the list
                    DisplayList(lines);
                }

                //If statement with code for insertion sort for if the user selects option 2
                else if(sortType == 2)
                {
                    InsertionSort(lines);
                    Console.WriteLine("The list was sorted using the insertion sort algorithm");

                    //making a new blank line
                    Console.WriteLine("\n");

                    int numberToFind = GetNumber("Which number are you searching for? ");
                    int indexToFind = BinarySearch(lines, numberToFind);


                    if (indexToFind == -1)
                    {
                        Console.WriteLine("The number {0} was not found", numberToFind);
                    }
                    else
                    {
                        Console.WriteLine("The number {0} was found at line {1} in the list", numberToFind, indexToFind);
                    }

                    Console.WriteLine("\n");

                    //Displaying the list
                    DisplayList(lines);
                }

                //If statement with code for quick sort for if the user selects option 3
                else if (sortType == 3)
                {
                    QuickSort(lines, 0, lines.Count - 1);
                    Console.WriteLine("The list was sorted using the quick sort algorithm");

                    //making a new blank line
                    Console.WriteLine("\n");

                    int numberToFind = GetNumber("Which number are you searching for? ");
                    int indexToFind = BinarySearch(lines, numberToFind);

                    if (indexToFind == -1)
                    {
                        Console.WriteLine("The number {0} was not found", numberToFind);
                    }
                    else
                    {
                        Console.WriteLine("The number {0} was found in line {1} in the list", numberToFind, indexToFind);
                    }

                    Console.WriteLine("\n");


                    //Displaying the list
                    DisplayList(lines);
                }
            }

            Console.ReadLine();
        }

        // Method for prompting and gettiing input from the user to choose the searching algorithm they want to use
        static int ChooseSearchType(int searchType)
        {
            bool valid;

            //A do-while loop with try and catch to force the user to input correct numbers for choosing the searching algorithm and handles errors
            do
            {
                try
                {
                    valid = true;
                    Console.WriteLine("Would you like to search for a number using Linear Search or Binary Search?");
                    Console.WriteLine("Press 1 for a Linear Search");
                    Console.WriteLine("Press 2 for a Binary Search");
                    searchType = int.Parse(Console.ReadLine());

                    if (searchType < 1 || searchType > 2)
                    {
                        Console.WriteLine("Invalid input! Please enter 1 or 2 to select the searching algorithm.");
                        valid = false;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Invalid number! Please enter numeric values only!");
                    valid = false;
                }

            }
            while (valid == false);

            return searchType;
        }

        // Method for prompting and gettiing input from the user to choose the sorting algorithm they want to use
        static int ChooseSortType(int sortType)
        {
            bool valid;
            //A do-while loop with try and catch to force the user to input correct numbers for choosing the sorting algorithm and handles errors
            do
            {
                try
                {
                    valid = true;
                    Console.WriteLine("Which sorting algorithm would you like to use for your Binary Search?");
                    Console.WriteLine("Press 1 to use Bubble Sort");
                    Console.WriteLine("Press 2 to use Insertion Sort");
                    Console.WriteLine("Press 3 to use Quick Sort");
                    sortType = int.Parse(Console.ReadLine());

                    if (sortType < 1 || sortType > 3)
                    {
                        Console.WriteLine("Invalid input! Please enter a number between 1 and 3 to select the sorting algorithm.");
                        valid = false;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Invalid number! Please enter numeric values only!");
                    valid = false;
                }
            }
            while (valid == false);

            return sortType;
        }

        //Method, nested with a do-whie=le and try and catch for getting a number from the user and making sure that it is in correct format
        static int GetNumber(string promp)
        {
            int number = 0;
            bool valid;

            //A do-while loop with try and catch to force the user to input correct number to search for in the list
            do
            {
                try
                {
                    valid = true;
                    Console.Write(promp);
                    number = int.Parse(Console.ReadLine());

                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Invalid input! Please only enter numeric data!");
                    valid = false;
                }
            }
            while (valid == false);

            return number;
        }

        //Method for Linear Search algorithm
        static void LinearSearch(int numberToFind, string filePath)
        {
            StreamReader myreader = new StreamReader(filePath);

            string currentRow;

            currentRow = myreader.ReadLine();

            while (!myreader.EndOfStream )
            {
                if (currentRow == numberToFind.ToString())
                {
                    Console.WriteLine("{0} was found!", numberToFind);
                    break;
                }
                else
                {
                    Console.WriteLine("{0} was not found!", numberToFind);
                    break;
                }
            }
        }

        //Method for Displaying the List to the user 
        static void DisplayList(List<string> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine("[{0}]{1} ", i, lines[i]);
            }
        }

        //Method for sorting the list with Bubble Sort
        static void BubbleSort(List<string> lines)
        {
            int temp;

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 1; j < lines.Count; j++)
                {
                    if (int.Parse(lines[j - 1]) > int.Parse(lines[j]))
                    {
                        //swapping the number at i and the number at j
                        temp = int.Parse(lines[j - 1]);
                        lines[j - 1] = lines[j];
                        lines[j] = temp.ToString();
                    }
                }
            }
        }

        //Method for sorting the list with Insertion Sort
        static void InsertionSort(List<string> lines)
        {
            int temp;

            for (int i = 0; i < lines.Count- 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (int.Parse(lines[j - 1]) > int.Parse(lines[j]))
                    {
                        //swapping the number at i and the number at j
                        temp = int.Parse(lines[j - 1]);
                        lines[j - 1] = lines[j];
                        lines[j] = temp.ToString();
                    }
                }
            }
        }

        //Method for sorting the list with Quick Sort
        static void QuickSort(List<string> lines, int start, int end)
        {
            int i;

            if (start < end)
            {
                i = Partition(lines, start, end);

                //Recursive quicksort method to check all arrays divided by pivot
                QuickSort(lines, start, i - 1);
                QuickSort(lines, i + 1, end);
            }
        }

        //Method for the Partition Method needed for the Quick Sort
        static int Partition(List<string> lines, int start, int end)
        {
            int temp;

            // pivot is a number that we select to divide our array into 2 parts
            int pivot = int.Parse(lines[end]);
            int i = start - 1;

            for (int x = start; x <= end - 1; x++)
            {
                if (int.Parse(lines[x]) <= pivot)
                {
                    //Incrementing the position of i and swapping the number at i and the number at x
                    i++;
                    temp = int.Parse(lines[i]);
                    lines[i] = lines[x];
                    lines[x] = temp.ToString();
                }
            }

            //swaping the first and last numbers
            temp = int.Parse(lines[i + 1]);
            lines[i + 1] = lines[end];
            lines[end] = temp.ToString();

            return i + 1;
        }

        //Method for Binary Search algorithm. This methods find the middle of the array and
        //then decides if the element it is looking for is bigger or smaller than the middle element
        static int BinarySearch(List<string> lines, int numberToFind)
        {
            int first, middle, last, index;
            bool found;

            first = 0;
            middle = 0;
            last = lines.Count - 1;
            index = 0;
            found = false;

            while (found == false && first <= last)
            {
                middle = (first + last) / 2;

                //if the number we're looking for is == to middle then found is = true, loop stops running.
                if (numberToFind == int.Parse(lines[middle]))
                {
                    found = true;
                }

                //if the number we're looking for is greater than the current middle number then add 1 to that middle index number.
                else if (numberToFind > int.Parse(lines[middle]))
                {
                    first = middle + 1;
                }

                //if the number is less than the current middle number then subtract one from the last index number.
                else if (numberToFind < int.Parse(lines[middle]))
                {
                    last = middle - 1;
                }
            }

            if (found == true)
            {
                index = middle;
            }
            else
            {
                index = -1;
            }

            return index;
        }
    }
}
