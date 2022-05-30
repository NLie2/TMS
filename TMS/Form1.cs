using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using System.Globalization;

//ToDo: Read CSV file as a list and transform into a 2dimensional array 

namespace TMS
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            // make example array
            const int teammembers = 5;

            List<int> list = new List<int> { 1, 2, 3, 4 };

            List<List<int>> exampledata1 = new List<List<int>>{ new List<int>{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
                                                               new List<int>{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                               new List<int>{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                               new List<int>{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                               new List<int>{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } 
                                                              };

            List<List<int>> exampledata2 = new List<List<int>>{new List<int>{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                                                               new List<int>{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                                                               new List<int>{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                                                               new List<int>{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                                                               new List<int>{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
                                                              };

            List<List<int>> exampledata3 = new List<List<int>>{new List<int>{ 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
                                                               new List<int>{ 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
                                                               new List<int>{ 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
                                                               new List<int>{ 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
                                                               new List<int>{ 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }
                                                              };

            List<List<int>> exampledata4 = new List<List<int>>{new List<int>{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                                                               new List<int>{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                                                               new List<int>{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                                                               new List<int>{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                                                               new List<int>{ 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }
                                                              };



            InitializeComponent();

            label1.Text = ListToArrayStr(ReadReplies()); 

            label2.Text = ListToArrayInt(exampledata4); 


            TMS t = new TMS(exampledata4, 5);

            double tmsScore = t.TMSscoreForTeamtotal();

            label3.Text = tmsScore.ToString();

            label4.Text = " cr: " + t.credibility.ToString() 
                           + " s: " + t.credibility.ToString() 
                           + " co: " + t.coordination.ToString() 
                           + " totalag: " + t.calculateAggregateScoreForscale().ToString()
                           + " total: " + t.TMSscoreForTeamtotal().ToString(); 

            //label4.Text = t.credibility.ToString();
            //label4.Text = t.calculateAggregateScorePeritem(teammembers, 1, 15).ToString(); 
            //label4.Text = t.normalizedScore(5, 1, 5).ToString(); 

        }




        private string ListToArrayInt(List<List<int>> table)
        {
            string result = "";

            foreach(List<int>  row in table)
            {
                foreach(int cell in row)
                {
                    result += cell.ToString(); 
                }
                result += "\n";
            }


            return result; 
        }

        private string ListToArrayStr(List<List<string>> table)
        {
            string result = "";

            foreach (List<string> row in table.Skip(1))
            {
                foreach (string cell in row.Skip(1).Take(15))
                {
                    result += cell;
                }
                result += "\n";
            }


            return result;
        }

        //load replies with streamreader STILL NEEDS TO BE MADE CORRECT/turn into a table
        private List<List<string>> ReadReplies()
        {
            string result = ""; 
           
            string filename = "responses.csv";

            
            var reader = new StreamReader(filename);


            string line;
            char[] separators = {','};

            result = reader.ReadToEnd();




            List<List<string>> table = new List<List<string>>();

            string[] rows = result.Split('\n');
            foreach( string row in rows) 
            {
                string[] cells = row.Split(',');
                List<string> _row = new List<string>();
                foreach (string cell in cells)
                {
                    _row.Add(cell);
                }
                table.Add(_row);
            }

            //string[,] words = new string[rows.Length, DataGridCell.]


            //separate according to structure of csv file (then you can use what is written in the label or the csv file for calculation of the score as well . 
            //loop through the lines 


            return table; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public class TMS
        {
            //skip unnecessary data 

            public List<List<int>> TeamReplies; 
            public double credibility;
            public double specialization;
            public double coordination;
            int members;

            public TMS(List<List<int>> replies, int howManyTeamMembers)  // int[howManyTeamMembers, 15]
            {
                TeamReplies = replies;

                members = howManyTeamMembers;

                credibility = TMSscoreForTeamPercategory(members, 0, 4);
                specialization = TMSscoreForTeamPercategory(members, 5, 9);
                coordination = TMSscoreForTeamPercategory(members, 10, 14);

            }

            public int calculateAggregateScorePeritem(int TeamMembers, int from, int to)
            {
                int result = 0;

                for (int i = 0; i < TeamMembers; i++)
                {
                    for (int k = from; k < to; k++) //e.g.: from question 5-10
                    {
                        result += TeamReplies[i][k];
                    }
            
                }
                return result;
            }

            public double calculateAggregateScoreForscale() //sums score of 3 categories
            {
                double aggregateScore = this.credibility + this.specialization + this.coordination;
                return aggregateScore;
            }

            public int normalizedScore(int maxScore, int minScore, int aggregateScore)
            {
                int range = maxScore - minScore;
                int X = aggregateScore - minScore;
                int normalized = X / range;
                return normalized;
            }
 
            public double TMSscoreForTeamPercategory(int teamsize, int from, int to) //gives normalized aggregate score 
            {
                int minscore = teamsize * 1;
                int maxscore = teamsize * 5;

                double result = normalizedScore(maxscore, minscore, calculateAggregateScorePeritem(teamsize, from, to));

                return result;
            }

            public double TMSscoreForTeamtotal()
            {
                int min = 3; 
                int max = 15;

                double result = calculateAggregateScoreForscale() / (max-min) ;

                return result; 
            }

        }

    }
}
