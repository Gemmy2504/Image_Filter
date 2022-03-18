using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;

namespace ImageFilters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        byte[,] ImageMatrix;
        string ImagePath;
       
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImagePath = OpenedFilePath;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);

            }
        }
        static int trimValue, sortIndex, maxSizeIndex, filterIndex;
       
        
        static double  time1,time2,final;
   
        private void btnZGraph_Click(object sender, EventArgs e)
        {
            compnant();
            // Make up some data points from the N, N log(N) functions
        int N = 40;
        double[] Nu_Windows = new double[maxSizeIndex+1];
        double[] ExecutionTime1 = new double[N];
        double[] ExecutionTime2= new double[N];
        double[] ExecutionTime3 = new double[N];

            //for (int i = 0; i < N; i++)
            //{
            //    Nu_Windows[i] = i;
            //    ExecutionTime1[i] = i;
            //    ExecutionTime2[i] = i * Math.Log(i);
            //}
            //int N = 40;
            // double[] Nu_Windows = new double[N];
            // double[] ExecutionTime1 = new double[N];
            // double[] ExecutionTime2 = new double[N];
            // double[] ExecutionTime3 = new double[N];
            


             ZGraphForm ZGF;


            if (filterIndex == 0) { //if alpha select 1 if adaptive select 2
                                    //Create a graph and add two curves to it
                ///ALPHA
                ///if(
                ///
                int nu = maxSizeIndex;
                 for(int i = 0; i < N; i++)//O(N)
                 { 
                    if (nu >= 0)
                    {
                        Nu_Windows[i] = i;
                        nu--;
                    }

                    
                    ExecutionTime1[i] = (i * i * i);
                    ExecutionTime2[i] = (i*i*i)+i ;
                    ExecutionTime3[i] = ((i * i * i) * Math.Log(i));

                 }

                ZGF = new ZGraphForm("Execution Time Graph(ALpha)", "Window Size", "Execution time");
                ZGF.add_curve("K-Element(Alpha) = O(N^3)", Nu_Windows, ExecutionTime1, Color.Red);// kElement
                ZGF.add_curve("Count (Alpha) = O((N^3)+K) ", Nu_Windows, ExecutionTime2, Color.Blue); // Count sort
                ZGF.add_curve("Merge (Alpha) = O(N^3*Log(N))", Nu_Windows, ExecutionTime3, Color.YellowGreen);// merge Sort
                ZGF.Show();
            }
            else
            {//// for Adaptive

                int nu = maxSizeIndex;
                for (int i = 0; i < N; i++)//O(N)
                {
                    if (nu >= 0)
                    {
                        Nu_Windows[i] = i;
                        nu--;
                    }

                    ExecutionTime1[i] = (i * i * i*i* maxSizeIndex);
                    ExecutionTime2[i] = ((i * i*i ) + i) * maxSizeIndex;
                    ExecutionTime3[i] = ((i * i*i ) * Math.Log(i)) * maxSizeIndex;

                }
                ZGF = new ZGraphForm("Execution Time Graph(Adaptive)", "Window Size", "Execution time");
                ZGF.add_curve("Quick(Adaptive) = O(N^4)", Nu_Windows, ExecutionTime1, Color.Red);// quick
                ZGF.add_curve("Count(Adaptive) = O(N^4)", Nu_Windows, ExecutionTime2, Color.Blue); // Count sort
                ZGF.add_curve("Merge(Adaptive) = O(N^3*log(N)) ", Nu_Windows, ExecutionTime3, Color.YellowGreen);// merge Sort
                ZGF.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
       
       
      
        private void filterBTN_Click(object sender, EventArgs e)
        {
           
            compnant();
            if (ImagePath == null)
            {
                MessageBox.Show("please Select image ....");
            }

            ////////////
            ///
            /// 
            else
            {
                
                ImageMatrix = ImageOperations.OpenImage(ImagePath);
                int max_trim_value = (((maxSizeIndex * maxSizeIndex) - ((maxSizeIndex - 1) * (maxSizeIndex - 1) +1)) / 2) + 1;
                time1=Environment.TickCount;
                if (max_trim_value < trimValue)
                {
                    
                   MessageBox.Show("trim value to high...");
                    return;
                }
               else if (filterIndex == 0)
                {

                    Filters.AlphaTrim_Flter(ImageMatrix, maxSizeIndex, sortIndex, trimValue);//O(N*N*N*N)


                }
                else if (filterIndex == 1)
                {
                    Filters.Adaptive_Filter(ImageMatrix, maxSizeIndex, sortIndex);//O(N*N*N*N)+Recursion
                }
               
                ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
               time2= Environment.TickCount;
                final = time2 - time1;
                MessageBox.Show("Execution Time : " + final + " ms"
                    + "\ntime 1 = "+time1+"\nTime 2 = "+time2);

            }

            
            ////
        }

        void compnant()
        {
            
            trimValue = int.Parse(trimValueText.Text);
          
            ////if 0 => kTH Element
            ///if 1 => quick
            ///if 2 => Count
            ///
              sortIndex=sortComBox.SelectedIndex;

           ////////////////////////////////////////////
           //// if 0=> 3x3
           /// if 1 => 5x5 
           /// if 2 => 7x7
           /// 
           
            maxSizeIndex = int.Parse(maxSIzeText.Text);
            ///////////////////////////////////////////////

            //// if 0=> ALPHA
            ///if 1 => ADAPTIVE
            
            filterIndex=filterComBox.SelectedIndex;

            // max trim valuee conditions add it here

        }
    }
}