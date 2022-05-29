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
            InitializeComponent();

            label1.Text = ReadReplies(); //display replies with streamreader

            //make example array 
            int[,] exampledata = new int[5, 4]{ { 1, 2, 3, 4 }, { 1, 1, 1, 1 }, { 2, 2, 2, 2 }, { 3, 3, 3, 3 }, { 4, 4, 4, 4 } };

            int rowLength = exampledata.GetLength(0);
            int colLength = exampledata.GetLength(1);

            string label2text = ""; 

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    label2text += exampledata[i, j];
                }
                label2text += "\n";
            }

            label2.Text = label2text; 

        }

        //load replies with streamreader STILL NEEDS TO BE MADE CORRECT/turn into a table
        private string ReadReplies()
        {
            string result = ""; 
           
            string filename = "responses.csv";

            //List<dynamic> records = new List<dynamic>(); 

            /*using (var reader = new StreamReader(filename))
            {
                using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<dynamic>().ToList(); 
                }
            }*/

            /*List<string> listA = new List<string>();
            List<string> listB = new List<string>();

            using (var reader = new StreamReader(filename))
            {
                int nr = 0; 
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listA.Add(values[nr]);
                    listB.Add(values[nr]);

                    nr++; 
                }


            }

            foreach (string a in listA)
            {
                result += a; 
            }*/

            var reader = new StreamReader(filename);


            string line;
            char[] separators = {','};

            result = reader.ReadToEnd();

            for (int i = 0; i< result.Length; i++)
            {
                string[] words = result.Split(separators);
            }

            //separate according to structure of csv file (then you can use what is written in the label or the csv file for calculation of the score as well . 
            //loop through the lines 


            return result; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public class TMS
        {
            public int[,] TeamReplies;
            public int credibility;
            public int specialization;
            public int coordination;

            int members;

            public TMS(int[,] replies, int howManyTeamMembers)  // int[howManyTeamMembers, 15]
            {
                TeamReplies = replies;

                members = howManyTeamMembers;
                credibility = calculateAggregateScorePeritem(members, 1, 5);
                specialization = calculateAggregateScorePeritem(members, 6, 10);
                coordination = calculateAggregateScorePeritem(members, 11, 15);

            }

            private int calculateAggregateScorePeritem(int TeamMembers, int from, int to)
            {
                int result = 0;

                for (int i = 0; i < TeamMembers; i++)
                {
                    for (int k = from; k < to; k++)//e.g.: from question 5-10
                    {
                        result += TeamReplies[i, k];
                    }
                }

                return result;
            }

            private int calculateAggregateScoreForscale(int specialization, int credibility, int coordination)
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
 
            private int TMSscoreForTeamPercategory(int teamsize, int from, int to) //E.g.: from= 5, to= 10
            {
                int minscore = teamsize * 1;
                int maxscore = teamsize * 5;

                int result = normalizedScore(minscore, maxscore, calculateAggregateScorePeritem(teamsize, from, to));

                return result;
            }

            private int TMSscoreForTeamtotal(int specialization, int credibility, int coordination)
            {
                int minscore = members * 1 * 3;
                int maxscore = members * 5 * 3;

                int result = normalizedScore(minscore, maxscore, calculateAggregateScoreForscale(specialization, credibility, coordination));

                return result; 
            }

        }

    }
}
