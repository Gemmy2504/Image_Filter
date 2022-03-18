using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace ImageFilters
{
    internal static class Filters
    {

        public static byte[,] Adaptive_Filter(byte[,] ImageMatrix, int Max_Size, int Sort)//O(N*N)* Function
        {

            byte[,] newImageMatrix = ImageMatrix;
            for (int y = 0; y < ImageMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < ImageMatrix.GetLength(1); x++)
                {

                    newImageMatrix[y, x] = AdaptiveFilter(ImageMatrix, x, y, 3, Max_Size, Sort);//O(N*N)
                }
            }

            return newImageMatrix;
        }
        private static byte AdaptiveFilter(byte[,] ImageMatrix, int old_X, int old_Y, int Window, int Max_Window_Size, int Sort)
        {
            int size = Window * Window;
            byte[] arr = new byte[size];
            int[] width = new int[size];
            int[] hight = new int[size];
            int Index = 0;
            for (int y = -(Window / 2); y <= (Window / 2); y++)//O(K*K)
            {
                for (int x = -(Window / 2); x <= (Window / 2); x++)
                {
                    width[Index] = x;
                    hight[Index] = y;
                    Index++;
                }
            }
            byte Max = 0, Min = 255, Med, Z;
            int A1, A2, B1, B2, ArrayLength = 0, NewY, NewX;

            Z = ImageMatrix[old_Y, old_X];
            for (int i = 0; i < size; i++)
            {
                NewY = old_Y + hight[i];
                NewX = old_X + width[i];
                if (NewX >= 0 && NewX < ImageOperations.GetWidth(ImageMatrix) 
                    && NewY >= 0 && NewY < ImageOperations.GetHeight(ImageMatrix))
                {
                    arr[ArrayLength] = ImageMatrix[NewY, NewX];
                    if (arr[ArrayLength] > Max)
                        Max = arr[ArrayLength];
                    if (arr[ArrayLength] < Min)
                        Min = arr[ArrayLength];
                    ArrayLength++;
                }
            }

            if (Sort == 2)
            {
                Functions.countSort(arr); //O(N+K)
            }
            else if (Sort == 1)
            {
                Functions.quickSort1(arr, 0, (ArrayLength - 1));//O(N*Log(N))
            }
            else if (Sort == 3)
            {
                arr = Functions.mergeSort(arr, 0, (ArrayLength - 1)); //O(N * Log(N))
            }

            Min = arr[0];
            Med = arr[ArrayLength / 2];
            A1 = Med - Min;
            A2 = Max - Med;

            if (A1 > 0 && A2 > 0)
            {
                B1 = Z - Min;
                B2 = Max - Z;
                if (B1 > 0 && B2 > 0)
                    return Z;
                else
                    return Med;

            }
            else
            {
                Window += 2;
                if (Window <= Max_Window_Size)
                {
                    return AdaptiveFilter(ImageMatrix, old_X, old_Y, Window, Max_Window_Size, Sort);//O(N*N)+T(N-2)   
                }
                else
                {
                    return Med;
                }
            }
        }

        public static byte[,] AlphaTrim_Flter(byte[,] ImageMatrix, int Max_Size, int Sort, int trimValue)//O(N*N)* Function
        {
            byte[,] newImageMatrix = ImageMatrix;
            for (int y = 0; y < ImageMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < ImageMatrix.GetLength(1); x++)
                {

                    newImageMatrix[y, x] = AlphaTrimFilter(ImageMatrix, x, y,  Max_Size, Sort, trimValue);//O(N*N)


                }
            }
          

            return newImageMatrix;
        }
        public static byte AlphaTrimFilter(byte[,] ImageMatrix, int old_X, int old_Y, int Max_Window_Size, int Sort, int trimValue)//O(N*N)
        {
           
            int size;
            
            if (Max_Window_Size % 2 != 0)
            {
                size = Max_Window_Size * Max_Window_Size;
            }
            else
            {
                size = (Max_Window_Size + 1) * (Max_Window_Size + 1);
            }
            byte[] arr = new byte[size];
            int[] width = new int[size];
            int[] hight = new int[size];

            int Index = 0;
            //window of size max window size * max window size
            for (int y = -(Max_Window_Size / 2); y <= (Max_Window_Size / 2); y++)//O(N*N) 
            {
                for (int x = -(Max_Window_Size / 2); x <= (Max_Window_Size / 2); x++)
                {
                    width[Index] = x;
                    hight[Index] = y;
                    Index++;
                }
            }
            
         
            int NewY, NewX;
            
            
            for (int i = 0,q=0; i < Max_Window_Size * Max_Window_Size; i++)//O(N*N)
            {
                NewY = old_Y + hight[i];
                NewX = old_X + width[i];
                if (NewX >= 0 && NewX <ImageOperations.GetWidth(ImageMatrix)&& NewY >= 0 
                &&  NewY < ImageOperations.GetHeight(ImageMatrix))
                {
                    arr[q] = ImageMatrix[NewY, NewX];
                    q++;
                }
            }
            byte mean=200 ;
            byte[] newarr= arr;
           
            if (Sort == 3)
            {

                newarr = Functions.mergeSort(arr, 0, arr.Length-1); //O(N * Log(N))
                mean = (byte)Functions.mean(newarr);//O(N)
            }
            else if (Sort == 2)
            {
                Functions.countSort(arr);//O(N+K)
                mean =(byte) Functions.mean(arr);

            }
            else if(Sort==0)
            {
                try
                {
                    newarr = Functions.trim(arr, trimValue);// O(N)

                }
                catch (Exception ex)
                {

                }


                //arr = Functions.Ksmallest2(arr, trimValue); //O(N*N)

                mean = (byte)Functions.mean(newarr);
            }
            
            return mean;
        }
       
      
    }


}


