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
        string repliesasmatrixstring; 

        public Form1()
        {
            repliesasmatrixstring = ""; 
            //Exampledata: 
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


            //Print results in Form1
            InitializeComponent();

            label1.Text = "INPUT FROM CSV " + "\n" + "\n" + ListToArrayStr(ReadRepliesFromFile()); 

            label2.Text = "EXAMPLEDATA" + "\n" + "\n" + ListToArrayInt(exampledata4); 


            TMS t = new TMS(convertStringMatrixToIntMatrix(repliesasmatrixstring), 3);

            double tmsScore = t.TMSscoreForTeamtotal();

  
            label4.Text = " Credibility: " + t.credibility.ToString() 
                           + " Specialization: " + t.credibility.ToString() 
                           + " Coordination: " + t.coordination.ToString() 
                           + " TotalAggregateScore: " + t.calculateAggregateScoreForscale().ToString()
                           + " TotalScore: " + t.TMSscoreForTeamtotal().ToString(); 

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
            foreach (List<string> row in table.Skip(1)) //With skip and take, the irrelevant parts of the csv files are skipped. 
            {
                foreach (string cell in row.Skip(1).Take(15))
                {
                    repliesasmatrixstring += cell + " ";
                }
                repliesasmatrixstring += "\n";
            }

            return repliesasmatrixstring;
        }


        private List<List<int>> convertStringMatrixToIntMatrix(string stringinmatrixformat)
        {
            List<List<int>> result = new List<List<int>>();

            string[] rows = stringinmatrixformat.Split('\n');
            char[] charSeparators = new char[] { ' ' };
            foreach (string row in rows)
            {
                string[] cells = row.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                List<int> _row = new List<int>();

                foreach (string cell in cells)
                {
                    _row.Add(int.Parse(cell)); // convert string intput to integer !!!
                }
                result.Add(_row);
            }

            return result; 

        }

        private List<List<string>> ReadRepliesFromFile()
        {
            string result = ""; 
           
            string filename = "responses.2.csv";
            
            var reader = new StreamReader(filename);

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

            return table; 
        }


        public class TMS
        {
            public List<List<int>> TeamReplies; 
            public double credibility;
            public double specialization;
            public double coordination;
            int members;

            public TMS(List<List<int>> replies, int howManyTeamMembers)  //Constructor takes a 2DList with the replies as input, as well as how many members
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
