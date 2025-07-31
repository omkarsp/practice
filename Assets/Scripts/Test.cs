using UnityEngine;

public class Test : MonoBehaviour
{
    void Start(){
        Pyramid(new int[]{2,3,2,5,7});
    }
    
    public void Pyramid(int[] nums)
    {
        int max = 0;
        for(int i = 0; i < nums.Length; i++){
            if(max < nums[i]){
                max = nums[i];
            }    
        }
        
        // var matrix = new char[nums.Length][];
        //
        // for(int j = max-1; j >= 0; j--){
        //     for(int i = 0; i < matrix[0].Length; i++){
        //         matrix[j][i] = '*';
        //         Debug.Log(matrix[j][i].ToString());
        //     }
        // }
        
        // Generate the pyramid row by row.
        for (int row = max; row > 0; row--)
        {
            string line = ""; // Store the current row as a string.

            for (int col = 0; col < nums.Length; col++)
            {
                if (nums[col] >= row)
                {
                    line += "* "; // Add a star if column height >= current row number.
                }
                else
                {
                    line += "  "; // Add spaces if column height < current row number.
                }
            }

            // Print the row (or use Debug.Log for Unity).
            Debug.Log(line.TrimEnd());
        }

    }
}

//2 3 2 5 7
//
//        *
//        *
//      * *
//      * *
//  *   * *
//* * * * *
//* * * * *
 
