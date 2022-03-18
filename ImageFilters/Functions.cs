using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ImageFilters
{
    internal static class Functions
    {
        //// Quick Sort Algorithm
        private static int partition1(byte[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {
                while (arr[left] < pivot)
                {
                    left++;
                }
                while (arr[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    if (arr[left] == arr[right])
                    {
                        return right;
                    }
                    byte temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                {
                    return right;
                }
            }


        }
        public static void quickSort1(byte[] arr, int left, int right)// O(N*logN)
        {

            if (left < right)
            {
                int piv = partition1(arr, left, right);
                if (piv > 1)
                {
                    quickSort1(arr, left, piv - 1);
                }

                if (piv + 1 < right)
                {
                    quickSort1(arr, piv + 1, right);
                }

            }

        }
        /////////////
        ////     float before = time();
        ////     code
        ///      float after = time();
        ///      console.write(after - before);
        ////
        ///
        ///k Smallest Algorithm

        private static byte[] KSmallest(byte[] arr)
        {
            byte[] final = new byte[2];
            final[0] = 99;//smallest
            final[1] = 1;//largest

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > final[1])
                {
                    final[1] = arr[i];
                }
                if (arr[i] < final[0])
                {
                    final[0] = arr[i];
                }

            }
            // return the smallest value in the array in index 0 
            // and return largest value in the array in index 1
            return final;

        }

        ///trimmed mean
        ///edit

        public static byte[] trim(byte[] arr, int num) // O(N)+T(N-1) where n is trim value
        {
            if (num == 0)
            {
                return arr;
            }
            byte[] exclude = new byte[2];
            exclude = KSmallest(arr);//O(N )
            byte[] newarr = new byte[arr.Length - 2];
            for (int i = 0, j = 0; i < arr.Length; i++)//O(N)
            {
                if (exclude[0] == arr[i] || exclude[1] == arr[i])
                {
                    continue;
                }
                else
                {
                    newarr[j] = arr[i];
                    j++;
                }
            }
            return trim(newarr, num - 1);

        }
        //////median
        ///
        public static float median(byte[] data)//O(N+K)
        { 
                countSort(data);
          
                if (data.Length % 2 == 0)
                    return (data[(data.Length / 2) - 1] + data[data.Length / 2]) / 2;
                else
                    return data[data.Length / 2];
            
        }


        /////////////////////////////
        public static float mean(byte [] arr) //O(N)
        {
            float sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                    sum += arr[i];
            }
            return (sum /arr.Length );
        }

       

        /////////////////////////
        //// Counting Sort Algorithm

      
        /////////////////
     
        public static void countSort(byte[] arr)//O(N+K)
        {
            byte[] exclude = new byte[2];
            exclude = KSmallest(arr);

            int range = exclude[1] - exclude[0] + 1;
            byte[] count = new byte[range];
            byte[] output = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                count[arr[i] - exclude[0]]++;
            }
            for (int i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1];
            }
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                output[count[arr[i] - exclude[0]] - 1] = arr[i];
                count[arr[i] - exclude[0]]--;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = output[i];
            }
           
        }


        ////////////////////////


       
       
        /// ////////// Merge Sort
     
        private static void merge(byte[] arr, int l, int m, int r)
        {

            int n1 = m - l + 1;
            int n2 = r - m;
            byte[] L = new byte[n1];
            byte[] R = new byte[n2];
            int i, j;

            for (i = 0; i < n1; ++i)
            {
                L[i] = arr[l + i];
            }

            for (j = 0; j < n2; ++j)
            {
                R[j] = arr[m + 1 + j];
            }

            i = 0;
            j = 0;

            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

 
        public static byte[] mergeSort(byte[] arr, int l, int r)//// O(N*Log(N))
        {
            byte[] output = new byte[arr.Length];
            if (l < r)
            {
                
                int m = l + (r - l) / 2;

                mergeSort(arr, l, m);
                mergeSort(arr, m + 1, r);
                merge(arr, l, m, r);

            }

            for (int i = 0; i < arr.Length; i++)
            {
                output[i] = arr[i];
            }
            return output;

        }





    }






}
