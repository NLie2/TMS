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


           // TMS t = new TMS(convertStringMatrixToIntMatrix(repliesasmatrixstring), 3);
            TMS t = new TMS(exampledata3, 5);

            double tmsScore = t.TMSscoreForTeamtotal();

  
            label4.Text = " CredibilityAggregate: " + t.credibilityAggregateTeam.ToString()
                            + " SpecializationAggregate: " + t.credibilityAggregateTeam.ToString()
                            + " CoordinationAggregate: " + t.coordinationAggregateTeam.ToString()
                            
                            + " TotalAggregateScore: " + t.calculateAggregateScoreForscale().ToString()

                            + "\n" + "\n"

                            + " CredibilityNormal: " + t.credibilityNormal.ToString() 
                            + " SpecializationNormal: " + t.credibilityNormal.ToString() 
                            + " CoordinationNormal: " + t.coordinationNormal.ToString() 

                            + " TotalNormalizedScore: " + t.TMSscoreForTeamtotal().ToString(); 

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
                    _row.Add(int.Parse(cell)); // convert string intput to integer 
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

            int members;

            public double credibilityNormal;
            public double specializationNormal;
            public double coordinationNormal;


            public double credibilityAggregateTeam;
            public double specializationAggregateTeam;
            public double coordinationAggregateTeam;

            public TMS(List<List<int>> replies, int howManyTeamMembers)  //Constructor takes a 2DList with the replies as input, as well as how many members
            {
                TeamReplies = replies;

                members = howManyTeamMembers;

                credibilityNormal = TMSscorenormalizedTeamItem(members, 0, 5); 
                specializationNormal = TMSscorenormalizedTeamItem(members, 5, 10);
                coordinationNormal = TMSscorenormalizedTeamItem(members, 10, 15);

                credibilityAggregateTeam = calculateAggregateScorePeritem(members, 0, 5);
                specializationAggregateTeam = calculateAggregateScorePeritem(members, 5, 10);
                coordinationAggregateTeam = calculateAggregateScorePeritem(members, 10, 15); 

        }

            public double calculateAggregateScorePeritem(int TeamMembers, int from, int to) //Calculates aggregate score for entire team for 1 item. 
            {
                double result = 0;

                for (int i = 0; i < TeamMembers; i++)
                {
                    for (int k = from; k < to; k++) //e.g.: from question 5-9
                    {
                        result += TeamReplies[i][k];
                    }
            
                }
                return result;
            }

            public double calculateAggregateScoreForscale() //sums score of 3 categories
            {
                double aggregateScore = credibilityAggregateTeam + specializationAggregateTeam + coordinationAggregateTeam;

                return aggregateScore;
            }
            

            public double normalizedScore(int maxScore, int minScore, double aggregateScore)
            {
                int range = maxScore - minScore;
                double X = aggregateScore - minScore;
                double normalized = X / range;

                return normalized;
            }

            public double TMSscorenormalizedTeamItem(int teamsize, int from, int to) //gives normalized score for entire team for one item, 
            {
                int min = teamsize * 5 * 1; 
                int max = teamsize * 5 * 5;

                double X = calculateAggregateScorePeritem(teamsize, from, to);

                double Xnormalized = (X - min) / (max - min);
                return Xnormalized; 

            }


            public double TMSscoreForTeamtotal()
            {
                int min = members * 15 * 1; 
                int max = members * 15 * 5;

                double X = calculateAggregateScoreForscale(); 


                double Xnormalized = (X  - min) / (max-min) ;

                return Xnormalized; 
            }

        }

    }
}
