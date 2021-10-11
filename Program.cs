using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaration of variables
            bool condition = false;
            int NumTests;
            int NumStudents;

            //do while loop declaration
            do
            {
                //try... catch
                try
                {
                    //Asking for input - number of students
                    Console.WriteLine("Number of students: ");

                    //Declaring and storing the input taken into a variable
                    NumStudents = Convert.ToInt32(Console.ReadLine());

                    //Asking for input - number of tests
                    Console.WriteLine("Number of tests: ");


                    //Declaring and storing the input taken into a variable
                    NumTests = Convert.ToInt32(Console.ReadLine());

                    double[,] arrNum = CheckValidity(NumStudents, NumTests);

                    Console.WriteLine();
                    Console.WriteLine();


                    //Calling methods and displaying
                    Console.WriteLine("Lowest grade: " + getMin(arrNum));
                    Console.WriteLine("Highest grade: " + getMax(arrNum));

                    Console.WriteLine();

                    Console.WriteLine("Overall Grade Distribution: ");
                    GradeDistribution(arrNum);//Calling method, passing an arguement, which is an array

                    condition = true;

                }
                //catching the exception in variable e
                catch (Exception e)
                {
                    Console.WriteLine("ERROR, " + e.Message);
                    condition = false;
                }

            } while (condition == false);

            Console.Read();

        }// end of main


        //Method to check validity
        public static double[,] CheckValidity(int StudentsNum, int TestsNum)

        {
            //Declaring a multi-dimentional array
            double[,] arrNumStudAndTest = new double[StudentsNum, (TestsNum + 1)];

            //Declaring variable for storing average
            double average;

            //Declaring the for loop
            for (int x = 0; x < StudentsNum; x++)
            {
                //so that the sum does not add all test marks
                double sum = 0;

                for (int y = 0; y < TestsNum; y++)
                {

                    try
                    {
                        Console.WriteLine("Mark " + (y + 1) + ": ");
                        arrNumStudAndTest[x, y] = Convert.ToInt32(Console.ReadLine());

                        //checking for exceptions using the custom exception created
                        if (arrNumStudAndTest[x, y] > 100)
                        {
                            throw (new CheckException("Invalid. Test mark not in range (0 - 100)."));
                            arrNumStudAndTest[x, y] = 0;
                            Console.WriteLine();
                        }
                        else if (arrNumStudAndTest[x, y] < 0)
                        {
                            throw (new CheckException("Invalid. Test mark not in range (0 - 100)."));
                            arrNumStudAndTest[x, y] = 0;
                            Console.WriteLine();
                        }

                    }

                    catch (CheckException ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error, " + ex.Message);//output of custom exception error
                        y--;
                        Console.WriteLine();
                    }

                    catch (Exception e)
                    {

                        if (y == 0)
                        {
                            Console.WriteLine(e.Message);
                            y = -1;
                        }
                        else
                        {
                            Console.WriteLine(e.Message);
                            arrNumStudAndTest[x, y] = 0;
                            y--;
                        }
                    }

                    if (y == -1) { }
                    else
                    {
                        sum = sum + arrNumStudAndTest[x, y];

                        if (y == (TestsNum - 1))
                        {
                            average = sum / TestsNum;//calculating aerage
                            arrNumStudAndTest[x, y + 1] = Math.Round(average, 2);//round the average and inserting to end of array
                        }
                    }

                }// end of for loop - y

            }// end of for loop - x



            //Clears the console
            Console.Clear();

            //Outputing student and the number of tests - headings
            Console.Write("Student\t");

            //for loop declaration
            for (int loop = 1; loop <= TestsNum; loop++)
            {
                Console.Write("Test " + loop + "\t");
            }// end of for loop

            //display for headings, average and final grade
            Console.Write("Average" + "\t" + "Final letter grade");

            //Leaves a blank line, in console
            Console.WriteLine();

            //decalring for loops
            for (int i = 0; i < StudentsNum; i++)
            {
                Console.WriteLine("");

                for (int j = 0; j < (TestsNum + 1); j++)
                {

                    if (j == 0)
                    {
                        Console.Write((i + 1) + "\t" + arrNumStudAndTest[i, j] + "\t");
                    }
                    else
                    {
                        Console.Write(arrNumStudAndTest[i, j] + "\t");
                    }

                    if (j == TestsNum)
                    {
                        Console.Write(CheckAvgSymbol(arrNumStudAndTest[i, j]));
                    }



                }// end of for loop - y

            }// end of for loop - x

            return arrNumStudAndTest;

        }

        //Method to Check symbol against average
        public static string CheckAvgSymbol(double Avg)
        {

            String symbol = "";

            if ((Avg >= 0.0) && (Avg <= 59.0))
            {
                symbol = "F";
            }
            else if ((Avg >= 60.0) && (Avg <= 69.0))
            {
                symbol = "D";
            }
            else if ((Avg >= 70.0) && (Avg <= 79.0))
            {
                symbol = "C";
            }
            else if ((Avg >= 80.0) && (Avg <= 89.0))
            {
                symbol = "B";
            }
            else if ((Avg >= 90.0) && (Avg <= 100.0))
            {
                symbol = "A";
            }

            return symbol;

        }

        //Method to get the minimum mark of all tests
        public static double getMin(double[,] Min)
        {

            double MinVal = 100;

            for (int get_row = 0; get_row < Min.GetLength(0); get_row++)
            {

                for (int get_col = 0; get_col < Min.GetLength(1); get_col++)
                {

                    double FindMin = Min[get_row, get_col];

                    if (FindMin < MinVal)
                    {
                        MinVal = FindMin;
                    }

                }

            }

            return MinVal;

        }

        //Method to get the maximum mark of all tests
        public static double getMax(double[,] Min)
        {

            double MinVal = 0;

            for (int get_row = 0; get_row < Min.GetLength(0); get_row++)
            {

                for (int get_col = 0; get_col < Min.GetLength(1); get_col++)
                {

                    double FindMin = Min[get_row, get_col];

                    if (FindMin > MinVal)
                    {
                        MinVal = FindMin;
                    }
                }
            }

            return MinVal;

        }

        //Method to display the distribution of grades
        public static void GradeDistribution(double[,] GradeDis)
        {

            int[] arrGradeDis = new int[11];

            for (int a = 0; a < GradeDis.GetLength(0); a++)
            {

                for (int b = 0; b < GradeDis.GetLength(1) - 1; b++)
                {

                    if (GradeDis[a, b] >= 0 && GradeDis[a, b] <= 9)
                    {
                        arrGradeDis[0] += 1;
                    }
                    else if (GradeDis[a, b] >= 10 && GradeDis[a, b] <= 19)
                    {
                        arrGradeDis[1] += 1;
                    }
                    else if (GradeDis[a, b] >= 20 && GradeDis[a, b] <= 29)
                    {
                        arrGradeDis[2] += 1;
                    }
                    else if (GradeDis[a, b] >= 30 && GradeDis[a, b] <= 39)
                    {
                        arrGradeDis[3] += 1;
                    }
                    else if (GradeDis[a, b] >= 40 && GradeDis[a, b] <= 49)
                    {
                        arrGradeDis[4] += 1;
                    }
                    else if (GradeDis[a, b] >= 50 && GradeDis[a, b] <= 59)
                    {
                        arrGradeDis[5] += 1;
                    }
                    else if (GradeDis[a, b] >= 60 && GradeDis[a, b] <= 69)
                    {
                        arrGradeDis[6] += 1;
                    }
                    else if (GradeDis[a, b] >= 70 && GradeDis[a, b] <= 79)
                    {
                        arrGradeDis[7] += 1;
                    }
                    else if (GradeDis[a, b] >= 80 && GradeDis[a, b] <= 89)
                    {
                        arrGradeDis[8] += 1;
                    }
                    else if (GradeDis[a, b] >= 90 && GradeDis[a, b] <= 99)
                    {
                        arrGradeDis[9] += 1;
                    }
                    else if (GradeDis[a, b] == 100)
                    {
                        arrGradeDis[10] += 1;
                    }

                }//end of for b

            }//end of for a

            Console.WriteLine("  0-9: " + new string('*', arrGradeDis[0]));
            Console.WriteLine("10-19: " + new string('*', arrGradeDis[1]));
            Console.WriteLine("20-29: " + new string('*', arrGradeDis[2]));
            Console.WriteLine("30-39: " + new string('*', arrGradeDis[3]));
            Console.WriteLine("40-49: " + new string('*', arrGradeDis[4]));
            Console.WriteLine("50-59: " + new string('*', arrGradeDis[5]));
            Console.WriteLine("60-69: " + new string('*', arrGradeDis[6]));
            Console.WriteLine("70-79: " + new string('*', arrGradeDis[7]));
            Console.WriteLine("80-89: " + new string('*', arrGradeDis[8]));
            Console.WriteLine("90-99: " + new string('*', arrGradeDis[9]));
            Console.WriteLine("  100: " + new string('*', arrGradeDis[10]));

        }//end of GradeDistribution


    }// end of class program

    //Custom exception created by creating as separate
    public class CheckException : Exception
    {
        //Constructor
        public CheckException(String message) : base(message)
        {

        }

    }

}
