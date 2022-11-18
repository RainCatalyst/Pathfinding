using UnityEngine;

public class SortingBasics : MonoBehaviour
{
    // Sort an array of integers (THIS CHANGES THE ARRAY)
    public void Sort(int[] values)
    {
        // Go through all numbers in array
        for (int j = 0; j < values.Length; j++)
        {
            // This finds the lowest number in array and puts it into min variable
            // NOTE: We start with j (was 0 before) because all values before j should be sorted already
            int minIndex = j;
            // Go trough all numbers in the array (starting from the last unsorted index j)
            for (int i = j; i < values.Length; i++)
            {
                // Update the index of the minimum known number if it's less than the current one
                if (values[i] < values[minIndex])
                {
                    minIndex = i;
                }
            }
            // This swaps the minimum value (at index minIndex) with the last unsorted value (at index j)
            int temp = values[j];
            values[j] = values[minIndex];
            values[minIndex] = temp;
        }
    }
    
    private void Start()
    {
        // Create an array
        int[] values = { 1, 4, 2, 3 };
        // Call our sort function
        Sort(values);
        // Print all elements of the array (that are now sorted, hopefully)
        for (int i = 0; i < values.Length; i++)
            print(values[i]);
    }
}