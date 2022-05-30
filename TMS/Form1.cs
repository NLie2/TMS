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
//ToDo: Check if all the membervariables are correct in TMS class. (maybe need to be transformed to properties)

namespace TMS
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            // make example array
            const int teammembers = 5;

            List<int> list = new List<int> { 1, 2, 3, 4 };

            List<List<int>> exampledata = new List<List<int>>{ new List<int>{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
                                                               new List<int>{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                               new List<int>{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                               new List<int>{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                               new List<int>{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } 
                                                              };




            InitializeComponent();

            label2.Text = Exampledata(exampledata); 


            TMS t = new TMS(exampledata, 5);
            int tmsScore = t.TMSscoreForTeamtotal();
            label3.Text = tmsScore.ToString();  

            label4.Text = t.TMSscoreForTeamPercategory(teammembers, 1, 15).ToString(); //YOU LEFT HERE 
            //label4.Text = t.credibility.ToString();
            //label4.Text = t.calculateAggregateScorePeritem(teammembers, 1, 15).ToString(); 

        }




        private string Exampledata(List<List<int>> table)
        {

            //int rowLength = exampledata.GetLength(0);
            //int colLength = exampledata.GetLength(1);

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
            //2dimensional list instead of array 
            //skip unnecessary data 

            public List<List<int>> TeamReplies; 
            //public int[,] TeamReplies;
            public int credibility;
            public int specialization;
            public int coordination;
            int members;

            public TMS(List<List<int>> replies, int howManyTeamMembers)  // int[howManyTeamMembers, 15]
            {
                TeamReplies = replies;

                members = howManyTeamMembers;

                credibility = TMSscoreForTeamPercategory(members, 0, 4);
                specialization = TMSscoreForTeamPercategory(members, 5, 9);
                coordination = TMSscoreForTeamPercategory(members, 11, 14);

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

                result += TeamMembers; 
                return result;
            }

            private int calculateAggregateScoreForscale(int specialization, int credibility, int coordination) //sums score of 3 categories
            {
                int aggregateScore = credibility + specialization + coordination;
                return aggregateScore;
            }

            private int normalizedScore(int maxScore, int minScore, int aggregateScore)
            {
                int range = maxScore - minScore;
                int X = aggregateScore - minScore;
                int normalized = X / range;
                return normalized;
            }
 
            public int TMSscoreForTeamPercategory(int teamsize, int from, int to) //gives normalized aggregate score 
            {
                int minscore = teamsize * 1;
                int maxscore = teamsize * 5;

                int result = normalizedScore(minscore, maxscore, calculateAggregateScorePeritem(teamsize, from, to));

                return result;
            }

            public int TMSscoreForTeamtotal()
            {
                int minscore = members * 1 * 3;
                int maxscore = members * 5 * 3;

                int result = normalizedScore(minscore, maxscore, calculateAggregateScoreForscale(specialization, credibility, coordination));

                return result; 
            }

        }

    }
}
